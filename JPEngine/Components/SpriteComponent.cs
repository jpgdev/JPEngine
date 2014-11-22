using System;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{
    public class SpriteComponent : DrawableComponent
    {
        private Color _drawingColor = Color.White;
        private Rectangle? _drawnPortion; //TODO: Rename?
        private Vector2 _origin = Vector2.Zero; //Todo: Move this to Transform?
        private SpriteEffects _spriteEffects = SpriteEffects.None;
        //private float _zIndex;

        #region Properties

        public Color DrawingColor
        {
            get { return _drawingColor; }
            set { _drawingColor = value; }
        }

        /// <summary>
        ///     The inner part of the resource to draw. Note : Automatically set to Rectangle(0, 0, Width, Height) in the Start().
        /// </summary>
        public Rectangle? DrawnPortion
        {
            get { return _drawnPortion; }
            set { _drawnPortion = value; }
        }

        /// <summary>
        /// The origin used as the center for a rotation. Does not need to take the Transform.Scale into account.
        /// </summary>
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Texture2D Texture { get; set; }

        public SpriteEffects SpriteEffects
        {
            get { return _spriteEffects; }
            set { _spriteEffects = value; }
        }

        #endregion

        public SpriteComponent(Entity gameObject, Texture2D texture)
            : base(gameObject)
        {
            if(texture == null)
                throw new ArgumentNullException("texture");

            Texture = texture;
            _origin = new Vector2((float)texture.Width / 2, (float)texture.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //float zIndex = Engine.SpriteManager.GetZIndex(this);
            
            //Rectangle posRect = new Rectangle(
            //    (int) Transform.Position.X + (int) Origin.X,
            //    (int) Transform.Position.Y + (int) Origin.Y,
            //    Width,
            //    Height);

            spriteBatch.Draw(
                Texture,
                new Vector2(Transform.Position.X, Transform.Position.Y),
                DrawnPortion,
                _drawingColor,
                GameObject.Transform.Rotation,
                Origin,
                GameObject.Transform.Scale,
                _spriteEffects,
                Engine.SpriteManager.GetZIndex(this));

            //base.Draw();
        }

        //public static float GetZDelta()
        //{
        //    return (float)_rand.Next(1, 10000) / 1000000.0f;
        //}

        //private void UpdateZIndex()
        //{
        //    float z = 0.0f; //Note: 0.0f = front, 1.0f = back.
        //    z = 1.0f - (GameObject.Transform.Position.Y + Height);
        //    //z = 1.0f - ((float)(GameObject.Transform.Position.Y + Height) / (float)(TileMap.Height * Engine.TileHeight));

        //    z += _zDelta;
        //    ZIndex = Math.Min(z, 0.999f);
        //}
    }
}