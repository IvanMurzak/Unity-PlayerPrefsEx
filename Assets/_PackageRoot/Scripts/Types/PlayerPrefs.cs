namespace Extensions.Unity.PlayerPrefsEx
{
    public abstract class PlayerPrefs<T>
    {
        public string Key { get; private set; }
        public T DefaultValue { get; private set; }
        public T Value
        {
            get
            {
                if (UnityEngine.PlayerPrefs.HasKey(Key))
                {
                    return Deserialize(UnityEngine.PlayerPrefs.GetString(Key));
                }
                else
                {
                    return DefaultValue;
                }
            }
            set => UnityEngine.PlayerPrefs.SetString(Key, Serialize(value));
        }

        protected abstract string Serialize(T value);
        protected abstract T Deserialize(string value);

        public PlayerPrefs(string key, T defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
}