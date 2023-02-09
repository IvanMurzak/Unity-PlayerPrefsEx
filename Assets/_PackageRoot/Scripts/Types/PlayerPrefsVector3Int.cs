using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector3Int : PlayerPrefs<Vector3Int>
    {
        public PlayerPrefsVector3Int(string key, Vector3Int defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector3Int value) => PlayerPrefs.SerializeVector3Int(value);
        protected override Vector3Int Deserialize(string value) => PlayerPrefs.DeserializeVector3Int(value, DefaultValue);
    }
    public static partial class PlayerPrefs
    {
        public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = default)
        {
            var value = UnityEngine.PlayerPrefs.GetString(key, null);
            return DeserializeVector3Int(value, defaultValue);
        }
        public static void SetVector3Int(string key, Vector3Int value) => UnityEngine.PlayerPrefs.SetString(key, SerializeVector3Int(value));
        public static string SerializeVector3Int(Vector3Int value) => $"{value.x}:{value.y}:{value.z}";
        public static Vector3Int DeserializeVector3Int(string value, Vector3Int defaultValue)
        {
            if (value == null) return defaultValue;

            var strs = value.Split(':');
            if (strs.Length != 3) return defaultValue;

            int x, y, z;
            if (int.TryParse(strs[0], out x))
            {
                if (int.TryParse(strs[1], out y))
                {
                    if (int.TryParse(strs[2], out z))
                    {
                        return new Vector3Int(x, y, z);
                    }
                }
            }
            return defaultValue;
        }
    }
}