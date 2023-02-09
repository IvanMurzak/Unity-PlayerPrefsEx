namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefs
    {
        //
        // Summary:
        //     Returns true if the given key exists in PlayerPrefs, otherwise returns false.
        //
        // Parameters:
        //   key:
        public static bool HasKey(string key) => UnityEngine.PlayerPrefs.HasKey(key);
        //
        // Summary:
        //     Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey
        //     has no impact.
        //
        // Parameters:
        //   key:
        public static void DeleteKey(string key) => UnityEngine.PlayerPrefs.DeleteKey(key);
        //
        // Summary:
        //     Removes all keys and values from the preferences. Use with caution.
        public static void DeleteAll() => UnityEngine.PlayerPrefs.DeleteAll();
        //
        // Summary:
        //     Saves all modified preferences.
        public static void Save() => UnityEngine.PlayerPrefs.Save();
    }
}