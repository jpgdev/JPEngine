using System;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine.Components
{
    public interface IUpdateableComponent
    {
        bool Enabled { get; }

        int UpdateOrder { get; }

        event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;

        event EventHandler<ValueChangedEventArgs<int>> UpdateOrderChanged;
        
        void Update(GameTime gameTime);
    }
}