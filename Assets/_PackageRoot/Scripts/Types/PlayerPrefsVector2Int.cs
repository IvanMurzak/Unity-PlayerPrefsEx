using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector2Int : PlayerPrefs<Vector2Int>
    {
        public PlayerPrefsVector2Int(string key, Vector2Int defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector2Int value) => PlayerPrefs.SerializeVector2Int(value);
        protected override Vector2Int Deserialize(string value) => PlayerPrefs.DeserializeVector2Int(value, DefaultValue);
    }

    public static partial class PlayerPrefs
    {
        public static Vector2Int GetVector2Int(string key, Vector2Int defaultValue = default)
        {
            var value = UnityEngine.PlayerPrefs.GetString(key, null);
            return DeserializeVector2Int(value, defaultValue);
        }
        public static void SetVector2Int(string key, Vector2Int value) => UnityEngine.PlayerPrefs.SetString(key, SerializeVector2Int(value));
        public static string SerializeVector2Int(Vector2Int value) => $"{value.x}:{value.y}";
        public static Vector2Int DeserializeVector2Int(string value, Vector2Int defaultValue)
        {
            if (value == null) return defaultValue;

            var strs = value.Split(':');
            if (strs.Length != 2) return defaultValue;

            int x, y;
            if (int.TryParse(strs[0], out x))
            {
                if (int.TryParse(strs[1], out y))
                {
                    return new Vector2Int(x, y);
                }
            }
            return defaultValue;
        }
    }
}