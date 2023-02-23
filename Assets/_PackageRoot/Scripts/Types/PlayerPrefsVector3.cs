using UnityEngine;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsVector3 : IPlayerPrefsEx<Vector3>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public Vector3 DefaultValue { get; }
        public Vector3 Value
        {
            get => PlayerPrefsEx.GetInternalVector3(InternalKey, DefaultValue);
            set => PlayerPrefsEx.SetInternalVector3(InternalKey, value);
        }

        public PlayerPrefsVector3(string key, Vector3 defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<Vector3>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static Vector3 GetVector3(string key, Vector3 defaultValue = default) => GetInternalVector3(key.InternalKey<Vector3>(), defaultValue);
        public static void SetVector3(string key, Vector3 value) => SetInternalVector3(key.InternalKey<Vector3>(), value);
        internal static Vector3 GetInternalVector3(string InternalKey, Vector3 defaultValue = default)
        {
            var value = PlayerPrefs.GetString(InternalKey, null);
            return DeserializeVector3(value, defaultValue);
        }
        internal static void SetInternalVector3(string InternalKey, Vector3 value) => PlayerPrefs.SetString(InternalKey, SerializeVector3(value));
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