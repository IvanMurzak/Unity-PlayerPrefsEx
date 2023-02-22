using System;
using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
        public static T Get<T>(string key, T defaultValue = default) => GetEncrypted<T>(key.EncryptKey<T>(), defaultValue);
        public static void Set<T>(string key, T value) => SetEncrypted<T>(key.EncryptKey<T>(), value);

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

        //
        // Summary:
        //     Returns true if the given key exists in PlayerPrefs, otherwise returns false.
        //
        // Parameters:
        //   key:
        public static bool HasKey<T>(string key) => UnityEngine.PlayerPrefs.HasKey(key.EncryptKey<T>());
        //
        // Summary:
        //     Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey
        //     has no impact.
        //
        // Parameters:
        //   key:
        public static void DeleteKey<T>(string key) => UnityEngine.PlayerPrefs.DeleteKey(key.EncryptKey<T>());
        //
        // Summary:
        //     Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey
        //     has no impact.
        //
        // Parameters:
        //   key:
        //   type:
        public static void DeleteKey(string key, Type type) => UnityEngine.PlayerPrefs.DeleteKey(key.EncryptKey(type));
        //
        // Summary:
        //     Removes all keys and values from the preferences. Use with caution.
        public static void DeleteAll() => UnityEngine.PlayerPrefs.DeleteAll();
        //
        // Summary:
        //     Removes all keys and values of specific Type.
        // public static void DeleteAll<T>(string key) => UnityEngine.PlayerPrefs.DeleteKey(key.EncryptKey<T>());

        //
        // Summary:
        //     Saves all modified preferences.
        public static void Save() => UnityEngine.PlayerPrefs.Save();
    }
}