namespace JPEngine.Managers
{
    public interface IManager
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
