using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedVector3Int : IPlayerPrefsEx<Vector3Int>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector3Int DefaultValue { get; }

        public Vector3Int Value
        {
            get => PlayerPrefsEx.GetEncryptedVector3Int(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector3Int(Key, value);
        }

        public PlayerPrefsEncryptedVector3Int(string key, Vector3Int defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector3Int>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector3Int GetEncryptedVector3Int(string key, Vector3Int defaultValue = default)
        {
            var internalKey = key.InternalKey<Vector3Int>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            return DeserializeVector3Int(decrypted, defaultValue);
        }

        public static void SetEncryptedVector3Int(string key, Vector3Int value)
        {
            var internalKey = key.InternalKey<Vector3Int>();
            var serialized = SerializeVector3Int(value);
            var encrypted = PlayerPrefsExEncryptor.Encrypt(serialized);
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
