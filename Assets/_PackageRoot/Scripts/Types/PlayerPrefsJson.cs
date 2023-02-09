using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsJson<T> : PlayerPrefsEx<T>
    {
        public PlayerPrefsJson(string key, T defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(T value) => JsonUtility.ToJson(value);
        protected override T Deserialize(string value) => JsonUtility.FromJson<T>(value); 
    }
    public static partial class PlayerPrefsEx
    {
        public static T GetJson<T>(string key, T defaultValue = default)
        {
            var str = PlayerPrefs.GetString(key.EncryptKey<T>());
            if (string.IsNullOrEmpty(str))
                return defaultValue;

            return JsonUtility.FromJson<T>(str);
        }
        public static void SetJson<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key.EncryptKey<T>(), json);
        }
    }
}