using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsString : IPlayerPrefsEx<string>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public string DefaultValue { get; }

        public string Value
        {
            get => PlayerPrefsEx.GetString(Key, DefaultValue);
            set => PlayerPrefsEx.SetString(Key, value);
        }

        public PlayerPrefsString(string key, string defaultValue = "")
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<string>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static string GetString(string key, string defaultValue = "") => GetEncryptedString(key.EncryptKey<string>(), defaultValue);
        public static void SetString(string key, string value) => SetEncryptedString(key.EncryptKey<string>(), value);
        internal static string GetEncryptedString(string encryptedKey, string defaultValue = "") => PlayerPrefs.GetString(encryptedKey, defaultValue);
        internal static void SetEncryptedString(string encryptedKey, string value) => PlayerPrefs.SetString(encryptedKey, value);
    }
}