namespace Extensions.Unity.PlayerPrefsEx
{
    public static class PlayerPrefsExEncryptor
    {
#if UNITY_EDITOR
        public const int Hash = 666;
#else
	    public static readonly int Hash = UnityEngine.SystemInfo.deviceUniqueIdentifier.GetHashCode();
#endif
    }
}