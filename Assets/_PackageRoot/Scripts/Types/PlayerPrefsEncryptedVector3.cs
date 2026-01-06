using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedVector3 : IPlayerPrefsEx<Vector3>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector3 DefaultValue { get; }

        public Vector3 Value
        {
            get => PlayerPrefsEx.GetEncryptedVector3(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector3(Key, value);
        }

        public PlayerPrefsEncryptedVector3(string key, Vector3 defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector3>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector3 GetEncryptedVector3(string key, Vector3 defaultValue = default)
        {
            var internalKey = key.InternalKey<Vector3>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            return DeserializeVector3(decrypted, defaultValue);
        }

        public static void SetEncryptedVector3(string key, Vector3 value)
        {
            var internalKey = key.InternalKey<Vector3>();
            var serialized = SerializeVector3(value);
            var encrypted = PlayerPrefsExEncryptor.Encrypt(serialized);
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
