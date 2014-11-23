using System;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine.Components
{
    public interface IUpdateableComponent : IComponent
    {
        int UpdateOrder { get; }

        event EventHandler<ValueChangedEventArgs<int>> UpdateOrderChanged;
        
        void Update(GameTime gameTime);
    }
}