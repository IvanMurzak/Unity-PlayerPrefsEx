using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsBool
    {
        public string Key { get; private set; }
        public string EncryptedKey { get; private set; }
        public bool DefaultValue { get; private set; }
        public bool Value
        {
            get => PlayerPrefsEx.GetEncryptedBool(EncryptedKey, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedBool(EncryptedKey, value);
        }

        public PlayerPrefsBool(string key, bool defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<bool>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static bool GetBool(string key, bool defaultValue = default) => GetEncryptedBool(key.EncryptKey<bool>(), defaultValue);
        public static void SetBool(string key, bool value) => SetEncryptedBool(key.EncryptKey<bool>(), value);
        internal static bool GetEncryptedBool(string encryptedKey, bool defaultValue = default)
        {
            return PlayerPrefs.GetInt(encryptedKey, defaultValue ? 1 : 0) == 1;
        }
        internal static void SetEncryptedBool(string encryptedKey, bool value)
        {
            PlayerPrefs.SetInt(encryptedKey, value ? 1 : 0);
        }
    }
}