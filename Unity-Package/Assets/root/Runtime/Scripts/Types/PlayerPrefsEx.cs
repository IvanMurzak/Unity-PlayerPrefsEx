using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public abstract class PlayerPrefsEx<T> : IPlayerPrefsEx<T>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public T DefaultValue { get; }
        public T Value
        {
            get
            {
                if (PlayerPrefs.HasKey(InternalKey))
                {
                    return Deserialize(PlayerPrefs.GetString(InternalKey));
                }
                else
                {
                    return DefaultValue;
                }
            }
            set => PlayerPrefs.SetString(InternalKey, Serialize(value));
        }

        public PlayerPrefsEx(string key, T defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<T>();
            this.DefaultValue = defaultValue;
        }
        protected abstract T Deserialize(string str);
        protected abstract string Serialize(T value);
    }
}