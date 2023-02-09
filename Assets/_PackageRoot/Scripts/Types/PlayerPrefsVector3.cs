using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector3 : PlayerPrefsEx<Vector3>
    {
        public PlayerPrefsVector3(string key, Vector3 defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector3 value) => PlayerPrefsEx.SerializeVector3(value);
        protected override Vector3 Deserialize(string value) => PlayerPrefsEx.DeserializeVector3(value, DefaultValue);
    }
    public static partial class PlayerPrefsEx
    {
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default) => GetEncryptedVector3(key.EncryptKey<Vector3>(), defaultValue);
        public static void SetVector3(string key, Vector3 value) => SetEncryptedVector3(key.EncryptKey<Vector3>(), value);
        internal static Vector3 GetEncryptedVector3(string encryptedKey, Vector3 defaultValue = default)
        {
            var value = PlayerPrefs.GetString(encryptedKey, null);
            return DeserializeVector3(value, defaultValue);
        }
        internal static void SetEncryptedVector3(string encryptedKey, Vector3 value) => PlayerPrefs.SetString(encryptedKey, SerializeVector3(value));
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