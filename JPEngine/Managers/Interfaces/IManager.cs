using System;

namespace JPEngine.Managers
{
    public interface IManager : IDisposable
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
