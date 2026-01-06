using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedBool : IPlayerPrefsEx<bool>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public bool DefaultValue { get; }

        public bool Value
        {
            get => PlayerPrefsEx.GetEncryptedBool(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedBool(Key, value);
        }

        public PlayerPrefsEncryptedBool(string key, bool defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<bool>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static bool GetEncryptedBool(string key, bool defaultValue = default)
        {
            var internalKey = key.InternalKey<bool>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var encrypted = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(encrypted))
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (decrypted == null || !bool.TryParse(decrypted, out var result))
                return defaultValue;

            return result;
        }

        public static void SetEncryptedBool(string key, bool value)
        {
            var internalKey = key.InternalKey<bool>();
            var encrypted = PlayerPrefsExEncryptor.Encrypt(value.ToString());
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
