using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public struct RenderedText : IText<SpriteFont>
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Scale { get; set; }

        public Color Color { get; set; }

        public DrawingLayer Layer { get; set; }

        public SpriteFont Font { get; set; }

        public string Text { get; set; }
    }
}
