namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsFloat
    {
        public string Key { get; private set; }
        public float DefaultValue { get; private set; }
        public float Value
        {
            get => PlayerPrefs.GetFloat(Key, DefaultValue);
            set => PlayerPrefs.SetFloat(Key, value);
        }

        public PlayerPrefsFloat(string key, float defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static float GetFloat(string key, float defaultValue = default) => UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
        public static void SetFloat(string key, float value) => UnityEngine.PlayerPrefs.SetFloat(key, value);
    }
}