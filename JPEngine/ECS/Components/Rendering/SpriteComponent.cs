using System;
using JPEngine.Entities;
using JPEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Components
{
    public class SpriteComponent : DrawableComponent
    {
        private Color _color = Color.White;
        private Rectangle? _drawnPortion; //TODO: Rename?
        private Vector2 _origin = Vector2.Zero; //Todo: Move this to Transform?
        private SpriteEffects _spriteEffects = SpriteEffects.None;

        //private Sprite _sprite;
        //private float _zIndex;

        #region Properties

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
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

        public Vector2 Position
        {
            get { return Transform.Position; }
        }

        public Vector2 Scale
        {
            get { return Transform.Scale; }
        }

        public float Rotation
        {
            get { return Transform.Rotation; }
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

        public override void Draw(GameTime gameTime)
        {
            //float zIndex = Engine.SpriteManager.GetZIndex(this);
            
            //Rectangle posRect = new Rectangle(
            //    (int) Transform.Position.X + (int) Origin.X,
            //    (int) Transform.Position.Y + (int) Origin.Y,
            //    Width,
            //    Height);

            //TODO: Keep only one sprite and change it, do not recreate one
            Sprite s = new Sprite(Texture, Position, Color, Layer)
            {
                DrawnPortion = DrawnPortion,
                Rotation = GameObject.Transform.Rotation,
                Color = _color,
                Origin = Origin,
                Scale = GameObject.Transform.Scale,
                Layer = Layer
            };

            Engine.SpriteRenderer.Draw(s);

            //_spriteBatch.Draw(
            //    Texture,
            //    new Vector2(Transform.Position.X, Transform.Position.Y),
            //    DrawnPortion,
            //    _color,
            //    GameObject.Transform.Rotation,
            //    Origin,
            //    GameObject.Transform.Scale,
            //    _spriteEffects,
            //    Engine.SpriteManager.GetZIndex(this));

            //base.Draw();
        }
    }
}