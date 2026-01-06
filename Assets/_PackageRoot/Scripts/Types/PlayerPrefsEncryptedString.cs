using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedString : IPlayerPrefsEx<string>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public string DefaultValue { get; }

        public string Value
        {
            get => PlayerPrefsEx.GetEncryptedString(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedString(Key, value);
        }

        public PlayerPrefsEncryptedString(string key, string defaultValue = "")
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<string>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static string GetEncryptedString(string key, string defaultValue = "")
        {
            var internalKey = key.InternalKey<string>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var encrypted = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(encrypted))
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            return decrypted ?? defaultValue;
        }

        public static void SetEncryptedString(string key, string value)
        {
            var internalKey = key.InternalKey<string>();
            if (string.IsNullOrEmpty(value))
            {
                PlayerPrefs.SetString(internalKey, value);
                return;
            }

            var encrypted = PlayerPrefsExEncryptor.Encrypt(value);
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
