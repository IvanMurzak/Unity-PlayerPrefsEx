using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedInt : IPlayerPrefsEx<int>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public int DefaultValue { get; }

        public int Value
        {
            get => PlayerPrefsEx.GetEncryptedInt(Key, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedInt(Key, value);
        }

        public PlayerPrefsEncryptedInt(string key, int defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<int>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static int GetEncryptedInt(string key, int defaultValue = default)
        {
            var internalKey = key.InternalKey<int>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var encrypted = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(encrypted))
                return defaultValue;

            var decrypted = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (decrypted == null || !int.TryParse(decrypted, out var result))
                return defaultValue;

            return result;
        }

        public static void SetEncryptedInt(string key, int value)
        {
            var internalKey = key.InternalKey<int>();
            var encrypted = PlayerPrefsExEncryptor.Encrypt(value.ToString());
            PlayerPrefs.SetString(internalKey, encrypted);
        }
    }
}
