using System;
using JPEngine.Events;

namespace JPEngine.Components
{
    public interface IComponent
    {
        bool Enabled { get; }

        bool Started { get; }

        string Tag { get; }

        void Initialize();

        void Start();

        //TODO: This is not forced, so it may not be added to the implementation, remove this or think about another solution.
        event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;
    }
}