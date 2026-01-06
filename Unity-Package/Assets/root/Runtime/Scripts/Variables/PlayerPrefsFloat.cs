using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsFloat : IPlayerPrefsEx<float>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public float DefaultValue { get; }
        public float Value
        {
            get => PlayerPrefsEx.GetInternalFloat(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalFloat(InternalKey, value);
        }

        public PlayerPrefsFloat(string key, float defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<float>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static float GetFloat(string key, float defaultValue = default) => GetInternalFloat(key.InternalKey<float>(), defaultValue);
        public static void SetFloat(string key, float value) => SetInternalFloat(key.InternalKey<float>(), value);
        internal static float GetInternalFloat(string InternalKey, float defaultValue = default) => PlayerPrefs.GetFloat(InternalKey, defaultValue);
        internal static void SetInternalFloat(string InternalKey, float value) => PlayerPrefs.SetFloat(InternalKey, value);
    }
}