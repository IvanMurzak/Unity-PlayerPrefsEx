using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedVector2Int : IPlayerPrefsEx<Vector2Int>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector2Int DefaultValue { get; }

        public Vector2Int Value
        {
            get => PlayerPrefsEx.GetEncryptedVector2Int(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector2Int(Key, value);
        }

        public PlayerPrefsEncryptedVector2Int(string key, Vector2Int defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector2Int>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector2Int GetEncryptedVector2Int(string key, Vector2Int defaultValue = default)
        {
            var internalKey = key.InternalKey<Vector2Int>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            return DeserializeVector2Int(decrypted, defaultValue);
        }

        public static void SetEncryptedVector2Int(string key, Vector2Int value)
        {
            var internalKey = key.InternalKey<Vector2Int>();
            var serialized = SerializeVector2Int(value);
            var encrypted = PlayerPrefsExEncryptor.Encrypt(serialized);
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
