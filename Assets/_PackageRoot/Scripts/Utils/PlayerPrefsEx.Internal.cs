using System;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
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