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

        internal static string InternalKey<T>(this string key) => $"{typeof(T).Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
        internal static string InternalKey(this string key, Type type) => $"{type.Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
    }
}