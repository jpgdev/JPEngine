namespace JPEngine.Managers
{
    public interface IResourceManager<out T> : IManager
    {
       bool IsResourceLoaded(string name);

        bool IsResourcePathAdded(string name);

        T this[string name] { get; }

        T GetResource(string name, bool forceLoad = false);

        string[] Loaded { get; }

        string[] Added { get; }

        bool Add(string name, string path, bool forceLoad = false);

        bool Remove(string name);

        bool Load(string name);

        bool Load(params string[] names);

        bool Unload(string name);

        bool Unload(params string[] names);

        void UnloadContent();
    }
}
