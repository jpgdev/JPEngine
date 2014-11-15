namespace JPEngine.Components
{
    public interface IComponent
    {
        string Tag { get; }

        void Initialize();

        void Start();

        //TODO: Put the Enabled here and add OnEnabled, OnDisabled
    }
}