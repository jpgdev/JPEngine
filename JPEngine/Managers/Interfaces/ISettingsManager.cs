namespace JPEngine.Managers
{
    public interface ISettingsManager : IManager
    {
        Setting this[string name] { get; }

        bool Add(Setting setting);

        bool Remove(string name);

        Setting Get(string name);

        bool Save(string path);

        bool Load(string path);
    }
}
