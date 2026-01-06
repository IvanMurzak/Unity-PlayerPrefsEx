using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedVector2 : IPlayerPrefsEx<Vector2>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector2 DefaultValue { get; }

        public Vector2 Value
        {
            get => PlayerPrefsEx.GetEncryptedVector2(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector2(Key, value);
        }

        public PlayerPrefsEncryptedVector2(string key, Vector2 defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector2>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector2 GetEncryptedVector2(string key, Vector2 defaultValue = default)
        {
            var internalKey = key.InternalKey<Vector2>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            return DeserializeVector2(decrypted, defaultValue);
        }

        public static void SetEncryptedVector2(string key, Vector2 value)
        {
            var internalKey = key.InternalKey<Vector2>();
            var serialized = SerializeVector2(value);
            var encrypted = PlayerPrefsExEncryptor.Encrypt(serialized);
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
