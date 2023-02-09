namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsString
    {
        public string Key { get; private set; }
        public string DefaultValue { get; private set; }
        public string Value
        {
            get => PlayerPrefs.GetString(Key, DefaultValue);
            set => PlayerPrefs.SetString(Key, value);
        }

        public PlayerPrefsString(string key, string defaultValue = "")
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static string GetString(string key, string defaultValue = "") => UnityEngine.PlayerPrefs.GetString(key, defaultValue);
        public static void SetString(string key, string value) => UnityEngine.PlayerPrefs.SetString(key, value);
    }
}