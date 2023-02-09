using System;
using System.Globalization;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsDateTime
    {
        public string Key { get; private set; }
        public DateTime DefaultValue { get; private set; }
        public DateTime Value
        {
            get => PlayerPrefs.GetDateTime(Key, DefaultValue);
            set => PlayerPrefs.SetDateTime(Key, value);
        }

        public PlayerPrefsDateTime(string key, DateTime defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static DateTime GetDateTime(string key, DateTime defaultValue = default)
        {
            var str = UnityEngine.PlayerPrefs.GetString(key, null);
            if (str == null) return defaultValue;

            long ticks;
            if (long.TryParse(str, out ticks))
                return new DateTime(ticks);

            return defaultValue;
        }
        public static void SetDateTime(string key, DateTime value)
        {
            UnityEngine.PlayerPrefs.SetString(key, value.Ticks.ToString());
        }
    }
}