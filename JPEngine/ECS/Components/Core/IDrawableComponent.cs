using System;
using JPEngine.Enums;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{
    public interface IDrawableComponent : IComponent
    {
        bool Visible { get; }

        DrawingLayer Layer { get; }

        event EventHandler<ValueChangedEventArgs<bool>> VisibleChanged;

        event EventHandler<ValueChangedEventArgs<DrawingLayer>> LayerChanged;

        void Draw(GameTime gameTime);
        //void Draw();

    }
}