using System;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx
{
    public class PlayerPrefsBigInt
    {
        public string Key { get; private set; }
        public BigInt DefaultValue { get; private set; }
        public BigInt Value
        {
            get => PlayerPrefs.GetBigInt(Key, DefaultValue);
            set => PlayerPrefs.SetBigInt(Key, value);
        }

        public PlayerPrefsBigInt(string key, BigInt defaultValue = default)
        {
            this.Key = key + PlayerPrefsEncryptor.Hash;
            this.DefaultValue = defaultValue;
        }
    }
    public static partial class PlayerPrefs
    {
        public static BigInt GetBigInt(string key, BigInt defaultValue = default)
        {
            var str = UnityEngine.PlayerPrefs.GetString(key, null);
            if (str == null) return defaultValue;

            BigInt result;
            if (BigInt.TryParse(str, out result))
                return result;
            return defaultValue;
        }
        public static void SetBigInt(string key, BigInt value)
        {
            UnityEngine.PlayerPrefs.SetString(key, value.ToString());
        }
    }
}