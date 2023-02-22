namespace Extensions.Unity.PlayerPrefsEx
{
    public interface IPlayerPrefsEx<T>
    {
        string Key { get; }
        string EncryptedKey { get; }
        T DefaultValue { get; }

        public T Value { get; set; }
    }
}