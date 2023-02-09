using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Tests
{
    public class PlayerPrefsStaticTest
    {
        const string Key = "PlayerPrefsEx-TestKey";

        [Test]
        public void DefaultValue()
        {
            PlayerPrefs.DeleteKey(Key);

            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key));
            
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key, null));
            
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key, default));
            
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key, ""));
            
            Assert.AreEqual("abc", 
                PlayerPrefs.GetString(Key, "abc"));
            
            Assert.AreEqual(1, 
                PlayerPrefs.GetInt(Key, 1));
            
            Assert.AreEqual(1f, 
                PlayerPrefs.GetFloat(Key, 1f));
            
            Assert.AreEqual(true, 
                PlayerPrefs.GetBool(Key, true));
            
            Assert.AreEqual(BigInt.One, 
                PlayerPrefs.GetBigInt(Key, BigInt.One));
            
            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100), 
                PlayerPrefs.GetDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100)));

            Assert.AreEqual(Vector2.one * 3, 
                PlayerPrefs.GetVector2(Key, Vector2.one * 3));

            Assert.AreEqual(Vector2Int.one * 3, 
                PlayerPrefs.GetVector2Int(Key, Vector2Int.one * 3));

            Assert.AreEqual(Vector3.one * 3, 
                PlayerPrefs.GetVector3(Key, Vector3.one * 3));

            Assert.AreEqual(Vector3Int.one * 3, 
                PlayerPrefs.GetVector3Int(Key, Vector3Int.one * 3));
        }

        [Test]
        public void InputOutputValuesAreEqual()
        {
            PlayerPrefs.DeleteKey(Key);

            PlayerPrefs.SetString(Key, "");
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key));

            PlayerPrefs.SetString(Key, null);
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key));

            PlayerPrefs.SetString(Key, default);
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key));

            PlayerPrefs.SetString(Key, "");
            Assert.AreEqual("", 
                PlayerPrefs.GetString(Key, ""));

            PlayerPrefs.SetString(Key, "abc");
            Assert.AreEqual("abc", 
                PlayerPrefs.GetString(Key));

            PlayerPrefs.SetInt(Key, 10);
            Assert.AreEqual(10, 
                PlayerPrefs.GetInt(Key));

            PlayerPrefs.SetFloat(Key, 10f);
            Assert.AreEqual(10f, 
                PlayerPrefs.GetFloat(Key));

            PlayerPrefs.SetBool(Key, true);
            Assert.AreEqual(true,
                PlayerPrefs.GetBool(Key));

            PlayerPrefs.SetBigInt(Key, BigInt.One * 10);
            Assert.AreEqual(BigInt.One * 10,
                PlayerPrefs.GetBigInt(Key));

            PlayerPrefs.SetDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100));
            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100),
                PlayerPrefs.GetDateTime(Key));

            PlayerPrefs.SetVector2(Key, Vector2.one * 3);
            Assert.AreEqual(Vector2.one * 3,
                PlayerPrefs.GetVector2(Key));

            PlayerPrefs.SetVector2Int(Key, Vector2Int.one * 3);
            Assert.AreEqual(Vector2Int.one * 3,
                PlayerPrefs.GetVector2Int(Key));

            PlayerPrefs.SetVector3(Key, Vector3.one * 3);
            Assert.AreEqual(Vector3.one * 3,
                PlayerPrefs.GetVector3(Key));

            PlayerPrefs.SetVector3Int(Key, Vector3Int.one * 3);
            Assert.AreEqual(Vector3Int.one * 3,
                PlayerPrefs.GetVector3Int(Key));
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstances()
        {
            PlayerPrefs.DeleteKey(Key);

        }
    }
}