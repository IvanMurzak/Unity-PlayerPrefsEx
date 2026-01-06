using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsBool : IPlayerPrefsEx<bool>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public bool DefaultValue { get; }

        public bool Value
        {
            get => PlayerPrefsEx.GetInternalBool(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalBool(InternalKey, value);
        }

        public PlayerPrefsBool(string key, bool defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<bool>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static bool GetBool(string key, bool defaultValue = default) => GetInternalBool(key.InternalKey<bool>(), defaultValue);
        public static void SetBool(string key, bool value) => SetInternalBool(key.InternalKey<bool>(), value);
        internal static bool GetInternalBool(string InternalKey, bool defaultValue = default)
        {
            return PlayerPrefs.GetInt(InternalKey, defaultValue ? 1 : 0) == 1;
        }
        internal static void SetInternalBool(string InternalKey, bool value)
        {
            PlayerPrefs.SetInt(InternalKey, value ? 1 : 0);
        }
    }
}