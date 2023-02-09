using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector2 : PlayerPrefs<Vector2>
    {
        public PlayerPrefsVector2(string key, Vector2 defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector2 value) => PlayerPrefs.SerializeVector2(value);
        protected override Vector2 Deserialize(string value) => PlayerPrefs.DeserializeVector2(value, DefaultValue);
    }

    public static partial class PlayerPrefs
    {
        public static Vector2 GetVector2(string key, Vector2 defaultValue = default)
        {
            var value = UnityEngine.PlayerPrefs.GetString(key, null);
            return DeserializeVector2(value, defaultValue);
        }
        public static void SetVector2(string key, Vector2 value) => UnityEngine.PlayerPrefs.SetString(key, SerializeVector2(value));
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