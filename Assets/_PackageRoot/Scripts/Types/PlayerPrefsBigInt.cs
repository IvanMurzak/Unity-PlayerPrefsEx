using System;
using UnityEngine;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsBigInt : PlayerPrefsEx<BigInt>
    {
        public PlayerPrefsBigInt(string key, BigInt defaultValue = default) : base(key, defaultValue) { }

        protected override string Serialize(BigInt value) => PlayerPrefsEx.SerializeBigInt(value);
        protected override BigInt Deserialize(string value) => PlayerPrefsEx.DeserializeBigInt(value, DefaultValue);
    }
    public static partial class PlayerPrefsEx
    {
        public static BigInt GetBigInt(string key, BigInt defaultValue = default) => GetEncryptedBigInt(key.EncryptKey<BigInt>(), defaultValue);
        public static void SetBigInt(string key, BigInt value) => SetEncryptedBigInt(key.EncryptKey<BigInt>(), value);

        internal static BigInt GetEncryptedBigInt(string encryptedKey, BigInt defaultValue = default)
        {
            var str = PlayerPrefs.GetString(encryptedKey, null);
            if (str == null) return defaultValue;

            BigInt result;
            if (BigInt.TryParse(str, out result))
                return result;
            return defaultValue;
        }
        internal static void SetEncryptedBigInt(string encryptedKey, BigInt value)
        {
            PlayerPrefs.SetString(encryptedKey, value.ToString());
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