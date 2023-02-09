using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Tests
{
    public class PlayerPrefsStaticTest
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

        [Test]
        public void DefaultValue()
        {
            DeleteKeyAllTypes(Key);

            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key));
            
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key, null));
            
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key, default));
            
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key, ""));
            
            Assert.AreEqual("abc", 
                PlayerPrefsEx.GetString(Key, "abc"));
            
            Assert.AreEqual(1, 
                PlayerPrefsEx.GetInt(Key, 1));
            
            Assert.AreEqual(1f, 
                PlayerPrefsEx.GetFloat(Key, 1f));
            
            Assert.AreEqual(true, 
                PlayerPrefsEx.GetBool(Key, true));
            
            Assert.AreEqual(BigInt.One, 
                PlayerPrefsEx.GetBigInt(Key, BigInt.One));
            
            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100), 
                PlayerPrefsEx.GetDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100)));

            Assert.AreEqual(Vector2.one * 3, 
                PlayerPrefsEx.GetVector2(Key, Vector2.one * 3));

            Assert.AreEqual(Vector2Int.one * 3, 
                PlayerPrefsEx.GetVector2Int(Key, Vector2Int.one * 3));

            Assert.AreEqual(Vector3.one * 3, 
                PlayerPrefsEx.GetVector3(Key, Vector3.one * 3));

            Assert.AreEqual(Vector3Int.one * 3, 
                PlayerPrefsEx.GetVector3Int(Key, Vector3Int.one * 3));
        }

        [Test]
        public void InputOutputValuesAreEqual()
        {
            DeleteKeyAllTypes(Key);

            PlayerPrefsEx.SetString(Key, "");
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key));

            PlayerPrefsEx.SetString(Key, null);
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key));

            PlayerPrefsEx.SetString(Key, default);
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key));

            PlayerPrefsEx.SetString(Key, "");
            Assert.AreEqual("", 
                PlayerPrefsEx.GetString(Key, ""));

            PlayerPrefsEx.SetString(Key, "abc");
            Assert.AreEqual("abc", 
                PlayerPrefsEx.GetString(Key));

            PlayerPrefsEx.SetInt(Key, 10);
            Assert.AreEqual(10, 
                PlayerPrefsEx.GetInt(Key));

            PlayerPrefsEx.SetFloat(Key, 10f);
            Assert.AreEqual(10f, 
                PlayerPrefsEx.GetFloat(Key));

            PlayerPrefsEx.SetBool(Key, true);
            Assert.AreEqual(true,
                PlayerPrefsEx.GetBool(Key));

            PlayerPrefsEx.SetBigInt(Key, BigInt.One * 10);
            Assert.AreEqual(BigInt.One * 10,
                PlayerPrefsEx.GetBigInt(Key));

            PlayerPrefsEx.SetDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100));
            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100),
                PlayerPrefsEx.GetDateTime(Key));

            PlayerPrefsEx.SetVector2(Key, Vector2.one * 3);
            Assert.AreEqual(Vector2.one * 3,
                PlayerPrefsEx.GetVector2(Key));

            PlayerPrefsEx.SetVector2Int(Key, Vector2Int.one * 3);
            Assert.AreEqual(Vector2Int.one * 3,
                PlayerPrefsEx.GetVector2Int(Key));

            PlayerPrefsEx.SetVector3(Key, Vector3.one * 3);
            Assert.AreEqual(Vector3.one * 3,
                PlayerPrefsEx.GetVector3(Key));

            PlayerPrefsEx.SetVector3Int(Key, Vector3Int.one * 3);
            Assert.AreEqual(Vector3Int.one * 3,
                PlayerPrefsEx.GetVector3Int(Key));
        }
        [Test]
        public void SameKeyDifferentTypes()
        {
            DeleteKeyAllTypes(Key);

            var vDate = DateTime.MaxValue - TimeSpan.FromDays(1000);
            var vBigInt = BigInt.Parse("545456655456878999000");
            var vVector2 = Vector2.down;
            var vVector3 = Vector3.forward;

            PlayerPrefsEx.SetBigInt(Key, vBigInt);
            PlayerPrefsEx.SetDateTime(Key, vDate);
            PlayerPrefsEx.SetVector2(Key, vVector2);
            PlayerPrefsEx.SetVector3(Key, vVector3);

            Assert.AreEqual(vBigInt, PlayerPrefsEx.GetBigInt(Key, 123));
            Assert.AreEqual(vDate, PlayerPrefsEx.GetDateTime(Key));
            Assert.AreEqual(vVector2, PlayerPrefsEx.GetVector2(Key));
            Assert.AreEqual(vVector3, PlayerPrefsEx.GetVector3(Key));
        }
    }
}