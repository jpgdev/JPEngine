namespace JPEngine.ECS.Components
{
    public interface IEntityComponent
    {
        string Name { get; }

        void Initialize();

        void Start();

        //TODO: Put the Enabled here and add OnEnabled, OnDisabled
    }
}