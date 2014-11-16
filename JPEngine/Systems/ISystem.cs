using System;
using System.Collections.Generic;
using JPEngine.Components;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine.Systems
{
    public interface ISystem
    {
        bool Enabled { get; }

        bool Initialized { get; }

        /// <summary>
        /// The list of types of components used by the System.
        /// </summary>
        IEnumerable<Type> TypesUsed { get; }    //TODO: Rename this

        event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;

        void Initialize();

        void Update(Dictionary<Type, IEnumerable<IComponent>> components, GameTime gameTime);
    }
}
