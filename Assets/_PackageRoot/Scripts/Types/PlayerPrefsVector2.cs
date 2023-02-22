using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsVector2 : IPlayerPrefsEx<Vector2>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public Vector2 DefaultValue { get; }
        public Vector2 Value
        {
            get => PlayerPrefsEx.GetEncryptedVector2(EncryptedKey, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector2(EncryptedKey, value);
        }

        public PlayerPrefsVector2(string key, Vector2 defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<Vector2>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector2 GetVector2(string key, Vector2 defaultValue = default) => GetEncryptedVector2(key.EncryptKey<Vector2>(), defaultValue);
        public static void SetVector2(string key, Vector2 value) => SetEncryptedVector2(key.EncryptKey<Vector2>(), value);
        internal static Vector2 GetEncryptedVector2(string encryptedKey, Vector2 defaultValue = default)
        {
            var value = PlayerPrefs.GetString(encryptedKey, null);
            return DeserializeVector2(value, defaultValue);
        }
        internal static void SetEncryptedVector2(string encryptedKey, Vector2 value) => PlayerPrefs.SetString(encryptedKey, SerializeVector2(value));
        public static string SerializeVector2(Vector2 value) => $"{value.x}:{value.y}";
        public static Vector2 DeserializeVector2(string value, Vector2 defaultValue)
        {
            if (value == null) return defaultValue;

            var strs = value.Split(':');
            if (strs.Length != 2) return defaultValue;

            float x, y;
            if (float.TryParse(strs[0], out x))
            {
                if (float.TryParse(strs[1], out y))
                {
                    return new Vector2(x, y);
                }
            }
            return defaultValue;
        }
    }
}