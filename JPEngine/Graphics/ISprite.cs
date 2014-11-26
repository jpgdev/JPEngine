using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public interface ISprite
    {
        Texture2D Texture { get; }

        Vector2 Position { get; }

        Vector2 Origin { get; }

        Rectangle? DrawnPortion { get; }

        Vector2 Scale { get; }

        float Rotation { get; }

        DrawingLayer Layer { get; }

        Color Color { get; }
    }
}
