using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsInt : IPlayerPrefsEx<int>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public int DefaultValue { get; }
        public int Value
        {
            get => PlayerPrefsEx.GetInternalInt(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalInt(InternalKey, value);
        }

        public PlayerPrefsInt(string key, int defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<int>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static int GetInt(string key, int defaultValue = default) => GetInternalInt(key.InternalKey<int>(), defaultValue);
        public static void SetInt(string key, int value) => SetInternalInt(key.InternalKey<int>(), value);
        internal static int GetInternalInt(string InternalKey, int defaultValue = default) => PlayerPrefs.GetInt(InternalKey, defaultValue);
        internal static void SetInternalInt(string InternalKey, int value) => PlayerPrefs.SetInt(InternalKey, value);
    }
}