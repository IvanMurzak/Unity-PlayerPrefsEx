using System;
using UnityEngine;
using NUnit.Framework;
using BigInt = System.Numerics.BigInteger;

namespace Extensions.Unity.PlayerPrefsEx.Tests
{
    public class PlayerPrefsInstancesTest
    {
        const string Key = "PlayerPrefsEx-TestKey";

        [Test]
        public void SharedValueWithSameKeyBetweenInstancesString()
        {
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

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
            PlayerPrefs.DeleteKey(Key);

            var pp1 = new PlayerPrefsVector3Int(Key);
            var pp2 = new PlayerPrefsVector3Int(Key);

            pp1.Value = Vector3Int.left;
            pp2.Value = Vector3Int.up;

            Assert.AreEqual(pp1.Value, pp2.Value);

            pp1.Value = Vector3Int.down;

            Assert.AreEqual(pp1.Value, pp2.Value);
        }
    }
}