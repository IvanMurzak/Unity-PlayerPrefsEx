using System;
using System.Security.Cryptography;
using System.Text;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static class PlayerPrefsExEncryptor
    {
#if UNITY_EDITOR
        public const int Hash = 666;
        private const string EditorDeviceId = "UnityEditor_PlayerPrefsEx_DeviceId";
#else
        public static readonly int Hash = UnityEngine.SystemInfo.deviceUniqueIdentifier.GetHashCode();
#endif

        private static byte[] _encryptionKey;

        private static byte[] EncryptionKey
        {
            get
            {
                if (_encryptionKey == null)
                {
#if UNITY_EDITOR
                    _encryptionKey = DeriveKey(EditorDeviceId);
#else
                    _encryptionKey = DeriveKey(UnityEngine.SystemInfo.deviceUniqueIdentifier);
#endif
                }
                return _encryptionKey;
            }
        }

        private static byte[] DeriveKey(string deviceId)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(deviceId));
            }
        }

        /// <summary>
        /// Encrypts a string using AES-256 encryption with device-based key.
        /// </summary>
        /// <param name="plainText">The string to encrypt.</param>
        /// <returns>Base64-encoded encrypted string with IV prepended.</returns>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using (var aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    var plainBytes = Encoding.UTF8.GetBytes(plainText);
                    var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    // Prepend IV to encrypted data
                    var result = new byte[aes.IV.Length + encryptedBytes.Length];
                    Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }

        /// <summary>
        /// Decrypts a string that was encrypted using the Encrypt method.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted string.</param>
        /// <returns>The decrypted string, or null if decryption fails.</returns>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                using (var aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;

                    // Extract IV from the beginning
                    var iv = new byte[aes.BlockSize / 8];
                    var encryptedBytes = new byte[fullCipher.Length - iv.Length];

                    Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                    Buffer.BlockCopy(fullCipher, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch
            {
                // Return null if decryption fails (corrupted data, wrong key, etc.)
                return null;
            }
        }
    }
}
