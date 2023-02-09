namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsBool
    {
        public string Key { get; private set; }
        public bool DefaultValue { get; private set; }
        public bool Value
        {
            get => PlayerPrefs.GetBool(Key, DefaultValue);
            set => PlayerPrefs.SetBool(Key, value);
        }

        public PlayerPrefsBool(string key, bool defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static bool GetBool(string key, bool defaultValue = default)
        {
            return UnityEngine.PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        public static void SetBool(string key, bool value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }
}