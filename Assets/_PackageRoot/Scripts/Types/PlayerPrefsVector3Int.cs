using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsVector3Int : IPlayerPrefsEx<Vector3Int>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public Vector3Int DefaultValue { get; }
        public Vector3Int Value
        {
            get => PlayerPrefsEx.GetEncryptedVector3Int(EncryptedKey, DefaultValue);
            set => PlayerPrefsEx.SetEncryptedVector3Int(EncryptedKey, value);
        }

        public PlayerPrefsVector3Int(string key, Vector3Int defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<Vector3Int>();
            this.DefaultValue = defaultValue;
        }
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