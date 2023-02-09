using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public abstract class PlayerPrefsEx<T>
    {
        public string Key { get; private set; }
        public string EncryptedKey { get; private set; }
        public T DefaultValue { get; private set; }
        public T Value
        {
            get
            {
                if (PlayerPrefs.HasKey(EncryptedKey))
                {
                    return Deserialize(PlayerPrefs.GetString(EncryptedKey));
                }
                else
                {
                    return DefaultValue;
                }
            }
            set => PlayerPrefs.SetString(EncryptedKey, Serialize(value));
        }

        protected abstract string Serialize(T value);
        protected abstract T Deserialize(string value);

        public PlayerPrefsEx(string key, T defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<T>();
            this.DefaultValue = defaultValue;
        }
    }
}