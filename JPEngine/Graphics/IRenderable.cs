using JPEngine.Enums;
using Microsoft.Xna.Framework;

namespace JPEngine.Graphics
{
    public interface IRenderable
    {
        Vector2 Position { get; }

        float Rotation { get; }

        Vector2 Origin { get; }

        Vector2 Scale { get; }

        Color Color { get; }

        DrawingLayer Layer { get; }
    }


}
