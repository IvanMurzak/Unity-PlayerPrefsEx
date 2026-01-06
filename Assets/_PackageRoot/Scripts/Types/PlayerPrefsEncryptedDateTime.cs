using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedDateTime : IPlayerPrefsEx<DateTime>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public DateTime DefaultValue { get; }

        public DateTime Value
        {
            get => PlayerPrefsEx.GetEncryptedDateTime(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedDateTime(Key, value);
        }

        public PlayerPrefsEncryptedDateTime(string key, DateTime defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<DateTime>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static DateTime GetEncryptedDateTime(string key, DateTime defaultValue = default)
        {
            var internalKey = key.InternalKey<DateTime>();
            var encrypted = PlayerPrefs.GetString(internalKey, null);
            if (encrypted == null)
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (decrypted == null)
                return defaultValue;

            if (long.TryParse(decrypted, out var ticks))
                return new DateTime(ticks);

            return defaultValue;
        }

        public static void SetEncryptedDateTime(string key, DateTime value)
        {
            var internalKey = key.InternalKey<DateTime>();
            var encrypted = PlayerPrefsExEncryptor.Encrypt(value.Ticks.ToString());
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
