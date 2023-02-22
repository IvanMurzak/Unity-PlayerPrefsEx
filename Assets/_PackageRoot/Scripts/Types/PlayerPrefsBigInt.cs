using UnityEngine;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx
{
    public struct PlayerPrefsBigInt : IPlayerPrefsEx<BigInt>
    {
        public string Key { get; }
        public string EncryptedKey { get; }
        public BigInt DefaultValue { get; }
        public BigInt Value
        { 
            get => PlayerPrefsEx.GetBigInt(Key, DefaultValue);
            set => PlayerPrefsEx.SetBigInt(Key, value);
        }

        public PlayerPrefsBigInt(string key, BigInt defaultValue = default)
        {
            this.Key = key;
            this.EncryptedKey = key.EncryptKey<BigInt>();
            this.DefaultValue = defaultValue;
        }
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