namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsInt
    {
        public string Key { get; private set; }
        public int DefaultValue { get; private set; }
        public int Value
        {
            get => PlayerPrefs.GetInt(Key, DefaultValue);
            set => PlayerPrefs.SetInt(Key, value);
        }

        public PlayerPrefsInt(string key, int defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static int GetInt(string key, int defaultValue = default) => UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
        public static void SetInt(string key, int value) => UnityEngine.PlayerPrefs.SetInt(key, value);
    }
}