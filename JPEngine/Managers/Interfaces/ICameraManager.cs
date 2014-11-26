using JPEngine.Components;

namespace JPEngine.Managers
{
    public interface ICameraManager : IManager
    {
        ICamera Current { get; }

        bool SetCurrent(string tag);

        bool SetCurrent(ICamera camera);

        void AddCamera(ICamera camera);

        bool ContainsCamera(string tag);

        bool ContainsCamera(ICamera cam);

        ICamera GetCamera(string tag);

        bool RemoveCamera(string tag);

        bool RemoveCamera(ICamera camera);
    }
}
