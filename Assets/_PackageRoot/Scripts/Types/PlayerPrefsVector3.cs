using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector3 : PlayerPrefs<Vector3>
    {
        public PlayerPrefsVector3(string key, Vector3 defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector3 value) => PlayerPrefs.SerializeVector3(value);
        protected override Vector3 Deserialize(string value) => PlayerPrefs.DeserializeVector3(value, DefaultValue);
    }
    public static partial class PlayerPrefs
    {
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
        {
            var value = UnityEngine.PlayerPrefs.GetString(key, null);
            return DeserializeVector3(value, defaultValue);
        }
        public static void SetVector3(string key, Vector3 value) => UnityEngine.PlayerPrefs.SetString(key, SerializeVector3(value));
        public static string SerializeVector3(Vector3 value) => $"{value.x}:{value.y}:{value.z}";
        public static Vector3 DeserializeVector3(string value, Vector3 defaultValue)
        {
            if (value == null) return defaultValue;

            var strs = value.Split(':');
            if (strs.Length != 3) return defaultValue;

            float x, y, z;
            if (float.TryParse(strs[0], out x))
            {
                if (float.TryParse(strs[1], out y))
                {
                    if (float.TryParse(strs[2], out z))
                    {
                        return new Vector3(x, y, z);
                    }
                }
            }
            return defaultValue;
        }
    }
}