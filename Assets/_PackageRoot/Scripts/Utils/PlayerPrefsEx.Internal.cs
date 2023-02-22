using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
        internal static T GetEncrypted<T>(string encryptedKey, T defaultValue = default)
        {
            if (PlayerPrefs.HasKey(encryptedKey))
            {
                var str = PlayerPrefs.GetString(encryptedKey);
                if (string.IsNullOrEmpty(str))
                    return defaultValue;
                return JsonUtility.FromJson<T>(str);
            }
            else
            {
                return defaultValue;
            }
        }
        internal static void SetEncrypted<T>(string encryptedKey, T value) => PlayerPrefs.SetString(encryptedKey, JsonUtility.ToJson(value));

        internal static string EncryptKey<T>(this string key) => $"{typeof(T).Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
        internal static string EncryptKey(this string key, Type type) => $"{type.Name}:{key}:{PlayerPrefsExEncryptor.Hash}";
    }
}