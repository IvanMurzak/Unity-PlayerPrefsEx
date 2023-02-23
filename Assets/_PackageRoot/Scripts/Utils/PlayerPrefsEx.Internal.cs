using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
        internal static T GetInternal<T>(string internalKey, T defaultValue = default)
        {
            if (PlayerPrefs.HasKey(internalKey))
            {
                var str = PlayerPrefs.GetString(internalKey);
                if (string.IsNullOrEmpty(str))
                    return defaultValue;
                return JsonUtility.FromJson<T>(str);
            }
            else
            {
                return defaultValue;
            }
        }
        internal static void SetInternal<T>(string internalKey, T value) => PlayerPrefs.SetString(internalKey, JsonUtility.ToJson(value));

        internal static string InternalKey<T>(this string key)
        {
#if PLAYER_PREFS_UNIQUE_KEY
            return $"{type.Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
#else
            if (Settings.UniqueKeyPerDevice)
                return $"{typeof(T).Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
            return $"{typeof(T).Name}:{key}";
#endif
        }
        internal static string InternalKey(this string key, Type type)
        {
#if PLAYER_PREFS_UNIQUE_KEY
            return $"{type.Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
#else
            if (Settings.UniqueKeyPerDevice)
                return $"{type.Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
            return $"{type.Name}:{key}";
#endif
        }
    }
}