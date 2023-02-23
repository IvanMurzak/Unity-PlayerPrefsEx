using System;

namespace Extensions.Unity.PlayerPrefsEx
{
    public static partial class PlayerPrefsEx
    {
        public static class Settings
        {
            public static bool UniqueKeyPerDevice { get; set; } = false;
        }
    }
}