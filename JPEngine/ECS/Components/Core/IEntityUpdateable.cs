using System;
using Microsoft.Xna.Framework;

namespace JPEngine.ECS.Components
{
    public interface IEntityUpdateable
    {
        bool Enabled { get; }

        int UpdateOrder { get; }

        event EventHandler<EventArgs> EnabledChanged;

        event EventHandler<EventArgs> UpdateOrderChanged;

        void Update(GameTime gameTime);
    }
}