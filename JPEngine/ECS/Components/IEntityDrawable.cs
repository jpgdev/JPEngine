using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public interface IEntityDrawable
    {
        bool Visible { get; }

        int DrawOrder { get; }

        event EventHandler<EventArgs> VisibleChanged;

        event EventHandler<EventArgs> DrawOrderChanged;

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        //void Draw();
    }
}