using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Tests
{
    public class PlayerPrefsInstancesTest
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
        public void SharedValueWithSameKeyBetweenInstancesString()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsString(Key);
            var pp2 = new PlayerPrefsString(Key);

            pp1.Value = "abc";
            pp2.Value = "123";

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = "QWE";

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesInt()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsInt(Key);
            var pp2 = new PlayerPrefsInt(Key);

            pp1.Value = 123;
            pp2.Value = 9999;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = 100500;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesBool()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsBool(Key);
            var pp2 = new PlayerPrefsBool(Key);

            pp1.Value = true;
            pp2.Value = false;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = true;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesFloat()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsFloat(Key);
            var pp2 = new PlayerPrefsFloat(Key);

            pp1.Value = 10f;
            pp2.Value = 992f;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = -10f;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesBigInt()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsBigInt(Key);
            var pp2 = new PlayerPrefsBigInt(Key);

            pp1.Value = 10;
            pp2.Value = 992;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = -10;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesDateTime()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsDateTime(Key);
            var pp2 = new PlayerPrefsDateTime(Key);

            pp1.Value = DateTime.MaxValue - TimeSpan.FromDays(20);
            pp2.Value = DateTime.MaxValue - TimeSpan.FromDays(200);

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = DateTime.MaxValue - TimeSpan.FromDays(5000);

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesVector2()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsVector2(Key);
            var pp2 = new PlayerPrefsVector2(Key);

            pp1.Value = Vector2.left;
            pp2.Value = Vector2.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector2.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesVector2Int()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsVector2Int(Key);
            var pp2 = new PlayerPrefsVector2Int(Key);

            pp1.Value = Vector2Int.left;
            pp2.Value = Vector2Int.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector2Int.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesVector3()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsVector3(Key);
            var pp2 = new PlayerPrefsVector3(Key);

            pp1.Value = Vector3.left;
            pp2.Value = Vector3.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector3.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
        [Test]
        public void SharedValueWithSameKeyBetweenInstancesVector3Int()
        {
            DeleteKeyAllTypes(Key);

            var pp1 = new PlayerPrefsVector3Int(Key);
            var pp2 = new PlayerPrefsVector3Int(Key);

            pp1.Value = Vector3Int.left;
            pp2.Value = Vector3Int.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector3Int.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }

        [Test]
        public void NonSharedValueBetweenSameKeyDifferentTypes()
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

            var ppBigInt = new PlayerPrefsBigInt(Key, vBigInt);
            var ppBool = new PlayerPrefsBool(Key, vBool);
            var ppDateTime = new PlayerPrefsDateTime(Key, vDateTime);
            var ppFloat = new PlayerPrefsFloat(Key, vFloat);
            var ppInt = new PlayerPrefsInt(Key, vInt);
            var ppString = new PlayerPrefsString(Key, vString);
            var ppVector2 = new PlayerPrefsVector2(Key, vVector2);
            var ppVector2Int = new PlayerPrefsVector2Int(Key, vVector2Int);
            var ppVector3 = new PlayerPrefsVector3(Key, vVector3);
            var ppVector3Int = new PlayerPrefsVector3Int(Key, vVector3Int);

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
    }
}