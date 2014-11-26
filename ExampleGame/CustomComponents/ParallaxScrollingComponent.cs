using JPEngine;
using JPEngine.Components;
using JPEngine.Entities;
using JPEngine.Graphics;
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
            _paralaxPosition.X = Transform.Position.X + Engine.Cameras.Current.Position.X * ParallaxRatio;
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO: Keep only one sprite and change it, do not recreate one
            Sprite s = new Sprite(Texture, new Vector2(_paralaxPosition.X, Transform.Position.Y), Color, Layer)
            {
                DrawnPortion = DrawnPortion,
                Rotation = GameObject.Transform.Rotation,
                Color = Color,
                Origin = Origin,
                Scale = GameObject.Transform.Scale,
                Layer = Layer
            };

            Engine.SpriteRenderer.Draw(s);



            //spriteBatch.Draw(
            //    Texture,
            //    new Vector2(_paralaxPosition.X, Transform.Position.Y),
            //    DrawnPortion,
            //    Color,
            //    GameObject.Transform.Rotation,
            //    Origin,
            //    GameObject.Transform.Scale,
            //    SpriteEffects,
            //    Engine.SpriteManager.GetZIndex(this));
        }
    }
}
