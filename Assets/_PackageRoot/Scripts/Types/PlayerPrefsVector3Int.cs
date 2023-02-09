using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsVector3Int : PlayerPrefsEx<Vector3Int>
    {
        public PlayerPrefsVector3Int(string key, Vector3Int defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(Vector3Int value) => PlayerPrefsEx.SerializeVector3Int(value);
        protected override Vector3Int Deserialize(string value) => PlayerPrefsEx.DeserializeVector3Int(value, DefaultValue);
    }
    public static partial class PlayerPrefsEx
    {
        public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = default) => GetEncryptedVector3Int(key.EncryptKey<Vector3Int>(), defaultValue);
        public static void SetVector3Int(string key, Vector3Int value) => SetEncryptedVector3Int(key.EncryptKey<Vector3Int>(), value);
        internal static Vector3Int GetEncryptedVector3Int(string encryptedKey, Vector3Int defaultValue = default)
        {
            var value = PlayerPrefs.GetString(encryptedKey, null);
            return DeserializeVector3Int(value, defaultValue);
        }
        internal static void SetEncryptedVector3Int(string encryptedKey, Vector3Int value) => PlayerPrefs.SetString(encryptedKey, SerializeVector3Int(value));
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