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
        //
        // Summary:
        //     Returns Value from PlayerPrefs by specified Type and Key deserialized from Json. If nothing exists by the Key and Type returns defaultValue
        //
        // Parameters:
        //   key:
        //   defaultValue:
        public static T GetJson<T>(string key, T defaultValue = default)
        {
            var internalKey = key.InternalKey<T>();
            if (!PlayerPrefs.HasKey(internalKey))
                return defaultValue;

            var str = PlayerPrefs.GetString(internalKey);
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return JsonUtility.FromJson<T>(str);
        }
        // Summary:
        //     Set Value to PlayerPrefs by specified Type and Key serialized to Json.
        //
        // Parameters:
        //   key:
        //   value:
        public static void SetJson<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key.InternalKey<T>(), json);
        }
    }
}