using JPEngine;
using JPEngine.Components;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame.CustomComponents
{
    public class ParallaxScrollingComponent : SpriteComponent
    {
        private Vector2 _paralaxPosition;

        /// <summary>
        /// The difference between the world and the parallax background ( 0 = same, 1 = negate the camera movement completly).
        /// </summary>
        public float ParallaxRatio { get; set; }

        public ParallaxScrollingComponent(Entity gameObject, Texture2D texture) : base(gameObject, texture)
        {
            _paralaxPosition = Transform.Position;
            ParallaxRatio = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Find shorter way to get the camera..... cache it? But need to know it it changes...
            _paralaxPosition.X = Transform.Position.X + Engine.Cameras.Current.Position.X * ParallaxRatio;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(
                Texture,
                new Vector2(_paralaxPosition.X, Transform.Position.Y),
                DrawnPortion,
                DrawingColor,
                GameObject.Transform.Rotation,
                Origin,
                GameObject.Transform.Scale,
                SpriteEffects,
                Engine.SpriteManager.GetZIndex(this));
        }
    }
}
