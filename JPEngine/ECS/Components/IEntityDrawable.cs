using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public interface IEntityDrawable
    {
        bool Visible { get; }

        DrawingLayer Layer { get; }

        event EventHandler<EventArgs> VisibleChanged;

        event EventHandler<EventArgs> LayerChanged;

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        //void Draw();

    }
}