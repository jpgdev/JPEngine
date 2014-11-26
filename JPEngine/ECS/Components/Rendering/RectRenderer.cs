using System;
using JPEngine.Components;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{
    public class RectRenderer : SpriteComponent
    {
        public Rectangle RectangleToRender { get; set; }
        

        //public RectRenderer(Entity gameObject, Rectangle rectangle)
        //    : this(gameObject, rectangle, texture)
        //{
        //    if (texture == null)
        //        texture = new Texture2D(Engine.Window.GraphicsDevice, 1, 1);
        //    //throw new ArgumentNullException("texture");

        //    Texture = texture;
        //    RectangleToRender = rectangle;
        //    DrawingColor = new Color(Color.Teal, 125);
        //}

        public RectRenderer(Entity gameObject, Rectangle rectangle, Texture2D texture)
            : base(gameObject, texture)
        {
            //if (texture == null)
            //    texture = new Texture2D(Engine.Window.GraphicsDevice, 1, 1);
                //throw new ArgumentNullException("texture");

            Texture = texture;
            RectangleToRender = rectangle;
            Color = new Color(Color.Teal, 125);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //TODO: Handle rotation??
            //if (Texture != null)
                spriteBatch.Draw(
                    Texture,
                    RectangleToRender,
                    null,
                    Color,
                    0,
                    //new Vector2((float)RectangleToRender.Width / 2, (float)RectangleToRender.Height / 2),//TODO: Why is the Origin acting so weird?
                    Vector2.Zero,
                    SpriteEffects.None,
                    0);
        }
    }
}
