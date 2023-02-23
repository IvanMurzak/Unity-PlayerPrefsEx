using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsVector2 : IPlayerPrefsEx<Vector2>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector2 DefaultValue { get; }
        public Vector2 Value
        {
            get => PlayerPrefsEx.GetInternalVector2(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalVector2(InternalKey, value);
        }

        public PlayerPrefsVector2(string key, Vector2 defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector2>();
            this.DefaultValue = defaultValue;
        }
    }

    public static partial class PlayerPrefsEx
    {
        public static Vector2 GetVector2(string key, Vector2 defaultValue = default) => GetInternalVector2(key.InternalKey<Vector2>(), defaultValue);
        public static void SetVector2(string key, Vector2 value) => SetInternalVector2(key.InternalKey<Vector2>(), value);
        internal static Vector2 GetInternalVector2(string InternalKey, Vector2 defaultValue = default)
        {
            var value = PlayerPrefs.GetString(InternalKey, null);
            return DeserializeVector2(value, defaultValue);
        }
        internal static void SetInternalVector2(string InternalKey, Vector2 value) => PlayerPrefs.SetString(InternalKey, SerializeVector2(value));
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