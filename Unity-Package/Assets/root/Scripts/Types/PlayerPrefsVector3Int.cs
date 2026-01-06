using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsVector3Int : IPlayerPrefsEx<Vector3Int>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector3Int DefaultValue { get; }
        public Vector3Int Value
        {
            get => PlayerPrefsEx.GetInternalVector3Int(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalVector3Int(InternalKey, value);
        }

        public PlayerPrefsVector3Int(string key, Vector3Int defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector3Int>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = default) => GetInternalVector3Int(key.InternalKey<Vector3Int>(), defaultValue);
        public static void SetVector3Int(string key, Vector3Int value) => SetInternalVector3Int(key.InternalKey<Vector3Int>(), value);
        internal static Vector3Int GetInternalVector3Int(string InternalKey, Vector3Int defaultValue = default)
        {
            var value = PlayerPrefs.GetString(InternalKey, null);
            return DeserializeVector3Int(value, defaultValue);
        }
        internal static void SetInternalVector3Int(string InternalKey, Vector3Int value) => PlayerPrefs.SetString(InternalKey, SerializeVector3Int(value));
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