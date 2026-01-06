using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsEncryptedJson<T> : IPlayerPrefsEx<T>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public T DefaultValue { get; }

        public T Value
        {
            get
            {
                if (!PlayerPrefs.HasKey(InternalKey))
                    return DefaultValue;

                var encrypted = PlayerPrefs.GetString(InternalKey);
                if (string.IsNullOrEmpty(encrypted))
                    return DefaultValue;

                var json = PlayerPrefsExEncryptor.Decrypt(encrypted);
                if (string.IsNullOrEmpty(json))
                    return DefaultValue;

                return JsonUtility.FromJson<T>(json);
            }
            set
            {
                var json = JsonUtility.ToJson(value);
                var encrypted = PlayerPrefsExEncryptor.Encrypt(json);
                PlayerPrefs.SetString(InternalKey, encrypted);
            }
        }

        public PlayerPrefsEncryptedJson(string key, T defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<T>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static T GetEncryptedJson<T>(string key, T defaultValue = default)
        {
            var internalKey = key.InternalKey<T>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var encrypted = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(encrypted))
                return defaultValue;

            var json = PlayerPrefsExEncryptor.Decrypt(encrypted);
            if (string.IsNullOrEmpty(json))
                return defaultValue;

            return JsonUtility.FromJson<T>(json);
        }

        public static void SetEncryptedJson<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            var encrypted = PlayerPrefsExEncryptor.Encrypt(json);
            PlayerPrefs.SetString(key.InternalKey<T>(), encrypted);
        }
    }
}
