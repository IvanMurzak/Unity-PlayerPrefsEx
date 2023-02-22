using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsDateTime : IPlayerPrefsEx<DateTime>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public DateTime DefaultValue { get; }

        public DateTime Value
        {
            get => PlayerPrefsEx.GetEncryptedDateTime(EncryptedKey, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedDateTime(EncryptedKey, value);
        }

        public PlayerPrefsDateTime(string key, DateTime defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<DateTime>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static DateTime GetDateTime(string key, DateTime defaultValue = default) => GetEncryptedDateTime(key.EncryptKey<DateTime>(), defaultValue);
        public static void SetDateTime(string key, DateTime value) => SetEncryptedDateTime(key.EncryptKey<DateTime>(), value);
        internal static DateTime GetEncryptedDateTime(string key, DateTime defaultValue = default)
        {
            var str = PlayerPrefs.GetString(key, null);
            if (str == null) return defaultValue;

            long ticks;
            if (long.TryParse(str, out ticks))
                return new DateTime(ticks);

            return defaultValue;
        }
        internal static void SetEncryptedDateTime(string key, DateTime value)
        {
            PlayerPrefs.SetString(key, value.Ticks.ToString());
        }
    }
}