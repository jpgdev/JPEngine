using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public class RectRenderer : DrawableSpriteComponent
    {
        public Rectangle RectangleToRender { get; set; }

        public RectRenderer(Entity gameObject, Rectangle rectangle, Texture2D texture)
            : base(gameObject, texture)
        {
            if (texture == null)
                throw new ArgumentNullException("The texture cannot be null.");

            Texture = texture;
            RectangleToRender = rectangle;
            DrawingColor = new Color(Color.Teal, 125);
        }

        //public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    //TODO: Handle rotation??
        //    if (Texture != null)
        //        spriteBatch.Draw(
        //            Texture,
        //            RectangleToRender,
        //            null,
        //            DrawingColor,
        //            0,
        //            new Vector2((float)RectangleToRender.Width / 2, (float)RectangleToRender.Height / 2),//TODO: Why is the Origin acting so weird?
        //            //Vector2.Zero,   
        //            SpriteEffects.None,
        //            0);
        //}
    }
}
