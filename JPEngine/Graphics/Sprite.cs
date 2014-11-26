using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public class Sprite : ISprite
    {
        public Vector2 Position { get; set; }

        public Rectangle Bounds { get; set; }

        public float Rotation { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Scale { get; set; }

        public Color Color { get; set; }

        public DrawingLayer Layer { get; set; }

        public Texture2D Texture { get; set; }

        public Rectangle? DrawnPortion { get; set; }
        
        public Sprite(Texture2D texture, Vector2 position, Color color, DrawingLayer layer)
            : this(texture, color, layer)
        {
            Position = position;
        }

        public Sprite(Texture2D texture, Rectangle bounds, Color color, DrawingLayer layer)
            : this(texture, color, layer)
        {
            Bounds = bounds;
        }

        private Sprite(Texture2D texture, Color color, DrawingLayer layer)
        {
            if(texture == null)
                throw new ArgumentNullException("texture");

            if (color == null)
                throw new ArgumentNullException("color");
            
            Texture = texture;
            Color = color;
            Layer = layer;
        }
    }
}
