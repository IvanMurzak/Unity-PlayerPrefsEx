using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsString : IPlayerPrefsEx<string>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public string DefaultValue { get; }

        public string Value
        {
            get => PlayerPrefsEx.GetString(Key, DefaultValue);
            set => PlayerPrefsEx.SetString(Key, value);
        }

        public PlayerPrefsString(string key, string defaultValue = "")
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<string>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static string GetString(string key, string defaultValue = "") => GetInternalString(key.InternalKey<string>(), defaultValue);
        public static void SetString(string key, string value) => SetInternalString(key.InternalKey<string>(), value);
        internal static string GetInternalString(string InternalKey, string defaultValue = "") => PlayerPrefs.GetString(InternalKey, defaultValue);
        internal static void SetInternalString(string InternalKey, string value) => PlayerPrefs.SetString(InternalKey, value);
    }
}