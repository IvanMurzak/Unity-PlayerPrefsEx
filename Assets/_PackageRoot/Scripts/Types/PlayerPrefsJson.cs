using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsJson<T> : IPlayerPrefsEx<T>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public T DefaultValue { get; }
        public T Value
        {
            get
            {
                if (PlayerPrefs.HasKey(InternalKey))
                {
                    return JsonUtility.FromJson<T>(PlayerPrefs.GetString(InternalKey));
                }
                else
                {
                    return DefaultValue;
                }
            }
            set => PlayerPrefs.SetString(InternalKey, JsonUtility.ToJson(value));
        }

        public PlayerPrefsJson(string key, T defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<T>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static T GetJson<T>(string key, T defaultValue = default)
        {
            var str = PlayerPrefs.GetString(key.InternalKey<T>());
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return JsonUtility.FromJson<T>(str);
        }
        public static void SetJson<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key.InternalKey<T>(), json);
        }
    }
}