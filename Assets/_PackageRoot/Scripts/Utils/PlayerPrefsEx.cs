using System;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
        //
        // Summary:
        //     Returns Value from PlayerPrefs by specified Type and Key. If nothing exists by the Key and Type returns defaultValue
        //
        // Parameters:
        //   key:
        //   defaultValue:
        public static T Get<T>(string key, T defaultValue = default) => GetInternal<T>(key.InternalKey<T>(), defaultValue);
        // Summary:
        //     Set Value to PlayerPrefs by specified Type and Key.
        //
        // Parameters:
        //   key:
        //   value:
        public static void Set<T>(string key, T value) => SetInternal<T>(key.InternalKey<T>(), value);
        //
        // Summary:
        //     Returns true if the given key exists in PlayerPrefs, otherwise returns false.
        //
        // Parameters:
        //   key:
        public static bool HasKey<T>(string key) => UnityEngine.PlayerPrefs.HasKey(key.InternalKey<T>());
        //
        // Summary:
        //     Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey
        //     has no impact.
        //
        // Parameters:
        //   key:
        public static void DeleteKey<T>(string key) => UnityEngine.PlayerPrefs.DeleteKey(key.InternalKey<T>());
        //
        // Summary:
        //     Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey
        //     has no impact.
        //
        // Parameters:
        //   key:
        //   type:
        public static void DeleteKey(string key, Type type) => UnityEngine.PlayerPrefs.DeleteKey(key.InternalKey(type));
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
        //
        // Summary:
        //     Returns internal key. The internal key used for saving in PlayerPrefs.
        //
        // Parameters:
        //   key:
        public static string GetInternalKey<T>(string key) => InternalKey<T>(key);
        //
        // Summary:
        //     Returns internal key. The internal key used for saving in PlayerPrefs.
        //
        // Parameters:
        //   key:
        //   type:
        public static string GetInternalKey(string key, Type type) => InternalKey(key, type);
    }
}