using UnityEngine;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsBigInt : IPlayerPrefsEx<BigInt>
    {
        public string Key { get; }
        public string InternalKey { get; }
        public BigInt DefaultValue { get; }
        public BigInt Value
        { 
            get => PlayerPrefsEx.GetBigInt(Key, DefaultValue);
            set => PlayerPrefsEx.SetBigInt(Key, value);
        }

        public PlayerPrefsBigInt(string key, BigInt defaultValue = default)
        {
            this.Key = key;
            this.InternalKey = key.InternalKey<BigInt>();
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefsEx
    {
        public static BigInt GetBigInt(string key, BigInt defaultValue = default) => GetInternalBigInt(key.InternalKey<BigInt>(), defaultValue);
        public static void SetBigInt(string key, BigInt value) => SetInternalBigInt(key.InternalKey<BigInt>(), value);

        internal static BigInt GetInternalBigInt(string InternalKey, BigInt defaultValue = default)
        {
            var str = PlayerPrefs.GetString(InternalKey, null);
            if (str == null) return defaultValue;

            BigInt result;
            if (BigInt.TryParse(str, out result))
                return result;
            return defaultValue;
        }
        internal static void SetInternalBigInt(string InternalKey, BigInt value)
        {
            PlayerPrefs.SetString(InternalKey, value.ToString());
        }

        public static string SerializeBigInt(BigInt value) => value.ToString();
        public static BigInt DeserializeBigInt(string value, BigInt defaultValue)
        {
            if (value == null) return defaultValue;

            BigInt x;
            if (BigInt.TryParse(value, out x))
            {
                return x;
            }
            return defaultValue;
        }
    }
}