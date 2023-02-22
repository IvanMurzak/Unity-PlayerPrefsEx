using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsDateTime : IPlayerPrefsEx<DateTime>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public DateTime DefaultValue { get; }

        public DateTime Value
        {
            get => PlayerPrefsEx.GetInternalDateTime(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalDateTime(InternalKey, value);
        }

        public PlayerPrefsDateTime(string key, DateTime defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<DateTime>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static DateTime GetDateTime(string key, DateTime defaultValue = default) => GetInternalDateTime(key.InternalKey<DateTime>(), defaultValue);
        public static void SetDateTime(string key, DateTime value) => SetInternalDateTime(key.InternalKey<DateTime>(), value);
        internal static DateTime GetInternalDateTime(string key, DateTime defaultValue = default)
        {
            var str = PlayerPrefs.GetString(key, null);
            if (str == null) return defaultValue;

            long ticks;
            if (long.TryParse(str, out ticks))
                return new DateTime(ticks);

            return defaultValue;
        }
        internal static void SetInternalDateTime(string key, DateTime value)
        {
            PlayerPrefs.SetString(key, value.Ticks.ToString());
        }
    }
}