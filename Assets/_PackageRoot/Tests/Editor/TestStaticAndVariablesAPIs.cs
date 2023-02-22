using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Tests
{
    public class TestStaticAndVariablesAPIs
    {
        const string Key = "PlayerPrefsEx-TestKey";

        Type[] types = new[] 
        {
            typeof(BigInt),
            typeof(bool),
            typeof(DateTime),
            typeof(float),
            typeof(int),
            typeof(string),
            typeof(Vector2),
            typeof(Vector2Int),
            typeof(Vector3),
            typeof(Vector3Int)
        };
        void DeleteKeyAllTypes(string key)
        {
            foreach (var type in types)
                PlayerPrefsEx.DeleteKey(key, type);
        }

        void SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs<T>(string key, Func<string, T, T> staticGetter, Action<string, T> staticSetter, IPlayerPrefsEx<T> variable, T value1, T value2)
        {
            Assert.AreEqual(variable.Value, staticGetter(key, default));
            variable.Value = value1;
            Assert.AreEqual(variable.Value, staticGetter(key, default));
            staticSetter(Key, value2);
            Assert.AreEqual(variable.Value, staticGetter(key, default));
        }

        [Test]
        public void SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs()
        {
            DeleteKeyAllTypes(Key);

            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetBigInt, PlayerPrefsEx.SetBigInt, new PlayerPrefsBigInt(Key), 123456, 38383838);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetBool, PlayerPrefsEx.SetBool, new PlayerPrefsBool(Key), true, false);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetDateTime, PlayerPrefsEx.SetDateTime, new PlayerPrefsDateTime(Key), DateTime.MaxValue - TimeSpan.FromDays(3), DateTime.MaxValue - TimeSpan.FromDays(3000));
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetFloat, PlayerPrefsEx.SetFloat, new PlayerPrefsFloat(Key), 123456.234f, 3838.002f);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetInt, PlayerPrefsEx.SetInt, new PlayerPrefsInt(Key), 123456, 38383838);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetString, PlayerPrefsEx.SetString, new PlayerPrefsString(Key), "123456", "38383838");
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetVector2, PlayerPrefsEx.SetVector2, new PlayerPrefsVector2(Key), Vector2.one * 100, Vector2.one * 100023);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetVector2Int, PlayerPrefsEx.SetVector2Int, new PlayerPrefsVector2Int(Key), Vector2Int.one * 100, Vector2Int.one * 100023);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetVector3, PlayerPrefsEx.SetVector3, new PlayerPrefsVector3(Key), Vector3.one * 100, Vector3.one * 100023);
            SharedValueByTheSameTypeAndKeyBetweenInstanceAndStaticAPIs(Key, PlayerPrefsEx.GetVector3Int, PlayerPrefsEx.SetVector3Int, new PlayerPrefsVector3Int(Key), Vector3Int.one * 100, Vector3Int.one * 100023);
        }
    }
}