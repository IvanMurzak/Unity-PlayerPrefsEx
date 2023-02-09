using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsString
    {
        public string Key { get; private set; }
        public string EncryptedKey { get; private set; }
        public string DefaultValue { get; private set; }
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