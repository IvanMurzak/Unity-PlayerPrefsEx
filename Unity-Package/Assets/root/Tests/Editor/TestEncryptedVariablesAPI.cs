using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Editor.Tests
{
    public class TestEncryptedVariablesAPI
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
        public void EncryptedSharedValueBetweenVariablesString()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedString(Key);
            var pp2 = new PlayerPrefsEncryptedString(Key);

            pp1.Value = "abc";
            pp2.Value = "123";

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = "QWE";

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesInt()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedInt(Key);
            var pp2 = new PlayerPrefsEncryptedInt(Key);

            pp1.Value = 123;
            pp2.Value = 9999;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = 100500;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesBool()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedBool(Key);
            var pp2 = new PlayerPrefsEncryptedBool(Key);

            pp1.Value = true;
            pp2.Value = false;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = true;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesFloat()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedFloat(Key);
            var pp2 = new PlayerPrefsEncryptedFloat(Key);

            pp1.Value = 10f;
            pp2.Value = 992f;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = -10f;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesBigInt()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedBigInt(Key);
            var pp2 = new PlayerPrefsEncryptedBigInt(Key);

            pp1.Value = 10;
            pp2.Value = 992;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = -10;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesDateTime()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedDateTime(Key);
            var pp2 = new PlayerPrefsEncryptedDateTime(Key);

            pp1.Value = DateTime.MaxValue - TimeSpan.FromDays(20);
            pp2.Value = DateTime.MaxValue - TimeSpan.FromDays(200);

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = DateTime.MaxValue - TimeSpan.FromDays(5000);

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesVector2()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedVector2(Key);
            var pp2 = new PlayerPrefsEncryptedVector2(Key);

            pp1.Value = Vector2.left;
            pp2.Value = Vector2.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector2.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesVector2Int()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedVector2Int(Key);
            var pp2 = new PlayerPrefsEncryptedVector2Int(Key);

            pp1.Value = Vector2Int.left;
            pp2.Value = Vector2Int.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector2Int.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesVector3()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedVector3(Key);
            var pp2 = new PlayerPrefsEncryptedVector3(Key);

            pp1.Value = Vector3.left;
            pp2.Value = Vector3.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector3.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedSharedValueBetweenVariablesVector3Int()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsEncryptedVector3Int(Key);
            var pp2 = new PlayerPrefsEncryptedVector3Int(Key);

            pp1.Value = Vector3Int.left;
            pp2.Value = Vector3Int.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector3Int.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void EncryptedNonSharedValueBetweenSameKeyDifferentTypes()
        {
            DeleteKeyAllTypes(Key);

            var vBigInt = BigInt.Parse("123123123123123123123123");
            var vBool = true;
            var vDateTime = DateTime.MinValue + TimeSpan.FromDays(10000);
            var vFloat = 23.2372f;
            var vInt = 235;
            var vString = "asdfjhk;lqwer";
            var vVector2 = Vector2.one * 123.123f;
            var vVector2Int = Vector2Int.one * 783;
            var vVector3 = Vector3.one * 3323.123f;
            var vVector3Int = Vector3Int.one * 2767;

            var ppBigInt = new PlayerPrefsEncryptedBigInt(Key, vBigInt);
            var ppBool = new PlayerPrefsEncryptedBool(Key, vBool);
            var ppDateTime = new PlayerPrefsEncryptedDateTime(Key, vDateTime);
            var ppFloat = new PlayerPrefsEncryptedFloat(Key, vFloat);
            var ppInt = new PlayerPrefsEncryptedInt(Key, vInt);
            var ppString = new PlayerPrefsEncryptedString(Key, vString);
            var ppVector2 = new PlayerPrefsEncryptedVector2(Key, vVector2);
            var ppVector2Int = new PlayerPrefsEncryptedVector2Int(Key, vVector2Int);
            var ppVector3 = new PlayerPrefsEncryptedVector3(Key, vVector3);
            var ppVector3Int = new PlayerPrefsEncryptedVector3Int(Key, vVector3Int);

            Assert.AreEqual(vBigInt, ppBigInt.Value);
            Assert.AreEqual(vBool, ppBool.Value);
            Assert.AreEqual(vDateTime, ppDateTime.Value);
            Assert.AreEqual(vFloat, ppFloat.Value);
            Assert.AreEqual(vInt, ppInt.Value);
            Assert.AreEqual(vString, ppString.Value);
            Assert.AreEqual(vVector2, ppVector2.Value);
            Assert.AreEqual(vVector2Int, ppVector2Int.Value);
            Assert.AreEqual(vVector3, ppVector3.Value);
            Assert.AreEqual(vVector3Int, ppVector3Int.Value);
        }

        [Test]
        public void EncryptedVariableValueIsNotPlainText()
        {
            DeleteKeyAllTypes(Key);

            var testValue = "SensitivePassword123";
            var pp = new PlayerPrefsEncryptedString(Key);
            pp.Value = testValue;

            // Get the raw stored value
            var storedValue = UnityEngine.PlayerPrefs.GetString(pp.InternalKey);

            // The stored value should not equal the plain text
            Assert.AreNotEqual(testValue, storedValue);

            // The decrypted value should equal the original
            Assert.AreEqual(testValue, pp.Value);
        }
    }
}
