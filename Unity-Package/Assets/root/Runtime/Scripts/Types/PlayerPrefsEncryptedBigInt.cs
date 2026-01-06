using UnityEngine;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedBigInt : IPlayerPrefsEx<BigInt>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public BigInt DefaultValue { get; }

        public BigInt Value
        {
            get => PlayerPrefsEx.GetEncryptedBigInt(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedBigInt(Key, value);
        }

        public PlayerPrefsEncryptedBigInt(string key, BigInt defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<BigInt>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static BigInt GetEncryptedBigInt(string key, BigInt defaultValue = default)
        {
            var internalKey = key.InternalKey<BigInt>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (decrypted == null)
                return defaultValue;

            if (BigInt.TryParse(decrypted, out var result))
                return result;

            return defaultValue;
        }

        public static void SetEncryptedBigInt(string key, BigInt value)
        {
            var internalKey = key.InternalKey<BigInt>();
            var encrypted = PlayerPrefsExEncryptor.Encrypt(value.ToString());
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
