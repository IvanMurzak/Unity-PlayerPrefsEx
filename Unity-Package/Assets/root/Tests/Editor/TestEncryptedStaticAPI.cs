using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Editor.Tests
{
    public class TestEncryptedStaticAPI
    {
        const string Key = "PlayerPrefsEx-EncryptedTestKey";

        readonly Type[] types = new[]
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
        public void EncryptedDefaultValue()
        {
            DeleteKeyAllTypes(Key);

            Assert.AreEqual("",
                PlayerPrefsEx.GetEncryptedString(Key));

            Assert.AreEqual("abc",
                PlayerPrefsEx.GetEncryptedString(Key, "abc"));

            Assert.AreEqual(1,
                PlayerPrefsEx.GetEncryptedInt(Key, 1));

            Assert.AreEqual(1f,
                PlayerPrefsEx.GetEncryptedFloat(Key, 1f));

            Assert.AreEqual(true,
                PlayerPrefsEx.GetEncryptedBool(Key, true));

            Assert.AreEqual(BigInt.One,
                PlayerPrefsEx.GetEncryptedBigInt(Key, BigInt.One));

            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100),
                PlayerPrefsEx.GetEncryptedDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100)));

            Assert.AreEqual(Vector2.one * 3,
                PlayerPrefsEx.GetEncryptedVector2(Key, Vector2.one * 3));

            Assert.AreEqual(Vector2Int.one * 3,
                PlayerPrefsEx.GetEncryptedVector2Int(Key, Vector2Int.one * 3));

            Assert.AreEqual(Vector3.one * 3,
                PlayerPrefsEx.GetEncryptedVector3(Key, Vector3.one * 3));

            Assert.AreEqual(Vector3Int.one * 3,
                PlayerPrefsEx.GetEncryptedVector3Int(Key, Vector3Int.one * 3));
        }

        [Test]
        public void EncryptedInputOutputValuesAreEqual()
        {
            DeleteKeyAllTypes(Key);

            PlayerPrefsEx.SetEncryptedString(Key, "abc");
            Assert.AreEqual("abc",
                PlayerPrefsEx.GetEncryptedString(Key));

            PlayerPrefsEx.SetEncryptedInt(Key, 10);
            Assert.AreEqual(10,
                PlayerPrefsEx.GetEncryptedInt(Key));

            PlayerPrefsEx.SetEncryptedFloat(Key, 10f);
            Assert.AreEqual(10f,
                PlayerPrefsEx.GetEncryptedFloat(Key));

            PlayerPrefsEx.SetEncryptedBool(Key, true);
            Assert.AreEqual(true,
                PlayerPrefsEx.GetEncryptedBool(Key));

            PlayerPrefsEx.SetEncryptedBigInt(Key, BigInt.One * 10);
            Assert.AreEqual(BigInt.One * 10,
                PlayerPrefsEx.GetEncryptedBigInt(Key));

            PlayerPrefsEx.SetEncryptedDateTime(Key, DateTime.MaxValue - TimeSpan.FromDays(100));
            Assert.AreEqual(DateTime.MaxValue - TimeSpan.FromDays(100),
                PlayerPrefsEx.GetEncryptedDateTime(Key));

            PlayerPrefsEx.SetEncryptedVector2(Key, Vector2.one * 3);
            Assert.AreEqual(Vector2.one * 3,
                PlayerPrefsEx.GetEncryptedVector2(Key));

            PlayerPrefsEx.SetEncryptedVector2Int(Key, Vector2Int.one * 3);
            Assert.AreEqual(Vector2Int.one * 3,
                PlayerPrefsEx.GetEncryptedVector2Int(Key));

            PlayerPrefsEx.SetEncryptedVector3(Key, Vector3.one * 3);
            Assert.AreEqual(Vector3.one * 3,
                PlayerPrefsEx.GetEncryptedVector3(Key));

            PlayerPrefsEx.SetEncryptedVector3Int(Key, Vector3Int.one * 3);
            Assert.AreEqual(Vector3Int.one * 3,
                PlayerPrefsEx.GetEncryptedVector3Int(Key));
        }

        [Test]
        public void EncryptedValueIsNotPlainText()
        {
            DeleteKeyAllTypes(Key);

            var testValue = "SensitivePassword123";
            PlayerPrefsEx.SetEncryptedString(Key, testValue);

            // Get the raw stored value
            var internalKey = PlayerPrefsEx.GetInternalKey<string>(Key);
            var storedValue = UnityEngine.PlayerPrefs.GetString(internalKey);

            // The stored value should not equal the plain text
            Assert.AreNotEqual(testValue, storedValue);

            // The decrypted value should equal the original
            Assert.AreEqual(testValue, PlayerPrefsEx.GetEncryptedString(Key));
        }

        [Test]
        public void EncryptedSameKeyDifferentTypes()
        {
            DeleteKeyAllTypes(Key);

            var vDate = DateTime.MaxValue - TimeSpan.FromDays(1000);
            var vBigInt = BigInt.Parse("545456655456878999000");
            var vVector2 = Vector2.down;
            var vVector3 = Vector3.forward;

            PlayerPrefsEx.SetEncryptedBigInt(Key, vBigInt);
            PlayerPrefsEx.SetEncryptedDateTime(Key, vDate);
            PlayerPrefsEx.SetEncryptedVector2(Key, vVector2);
            PlayerPrefsEx.SetEncryptedVector3(Key, vVector3);

            Assert.AreEqual(vBigInt, PlayerPrefsEx.GetEncryptedBigInt(Key, 123));
            Assert.AreEqual(vDate, PlayerPrefsEx.GetEncryptedDateTime(Key));
            Assert.AreEqual(vVector2, PlayerPrefsEx.GetEncryptedVector2(Key));
            Assert.AreEqual(vVector3, PlayerPrefsEx.GetEncryptedVector3(Key));
        }
    }
}
