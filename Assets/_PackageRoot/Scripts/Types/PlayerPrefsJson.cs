using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsJson<T> : IPlayerPrefsEx<T>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public T DefaultValue { get; }
        public T Value
        {
            get
            {
                if (PlayerPrefs.HasKey(EncryptedKey))
                {
                    return JsonUtility.FromJson<T>(PlayerPrefs.GetString(EncryptedKey));
                }
                else
                {
                    return DefaultValue;
                }
            }
            set => PlayerPrefs.SetString(EncryptedKey, JsonUtility.ToJson(value));
        }

        public PlayerPrefsJson(string key, T defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<T>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static T GetJson<T>(string key, T defaultValue = default)
        {
            var str = PlayerPrefs.GetString(key.EncryptKey<T>());
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return JsonUtility.FromJson<T>(str);
        }
        public static void SetJson<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key.EncryptKey<T>(), json);
        }
    }
}