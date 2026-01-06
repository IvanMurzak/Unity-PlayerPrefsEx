using System.Globalization;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedFloat : IPlayerPrefsEx<float>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public float DefaultValue { get; }

        public float Value
        {
            get => PlayerPrefsEx.GetEncryptedFloat(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedFloat(Key, value);
        }

        public PlayerPrefsEncryptedFloat(string key, float defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<float>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static float GetEncryptedFloat(string key, float defaultValue = default)
        {
            var internalKey = key.InternalKey<float>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var encrypted = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(encrypted))
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (decrypted == null || !float.TryParse(decrypted, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return defaultValue;

            return result;
        }

        public static void SetEncryptedFloat(string key, float value)
        {
            var internalKey = key.InternalKey<float>();
            var encrypted = PlayerPrefsExEncryptor.Encrypt(value.ToString(CultureInfo.InvariantCulture));
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
