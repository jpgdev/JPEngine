using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS.Components
{
    public class DrawableSpriteComponent : DrawableComponent
    {
        private Color _drawingColor = Color.White;
        private Rectangle _drawnPortion = Rectangle.Empty; //TODO: Rename?
        private Vector2 _origin = Vector2.Zero; //Todo: Move this to Transform?
        private SpriteEffects _spriteEffects = SpriteEffects.None;
        private float _zIndex;

        #region Properties

        public Color DrawingColor
        {
            get { return _drawingColor; }
            set { _drawingColor = value; }
        }

        /// <summary>
        ///     The inner part of the resource to draw. Note : Automatically set to Rectangle(0, 0, Width, Height) in the Start().
        /// </summary>
        public Rectangle DrawnPortion
        {
            get { return _drawnPortion; }
            set { _drawnPortion = value; }
        }

        //Todo: Move this to Transform?
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public string TextureName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public float ZIndex
        {
            get { return _zIndex; }
            set { _zIndex = value; }
        }

        public SpriteEffects SpriteEffects
        {
            get { return _spriteEffects; }
            set { _spriteEffects = value; }
        }

        #endregion

        public DrawableSpriteComponent(Entity gameObject)
            : base(gameObject)
        {
        }

        public override void Start()
        {
            //DrawnPortion = new Rectangle(0, 0, Width, Height);

            base.Start();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //TODO: Refactor all of this

            //todo: Check if the Texture has been loaded? or let it crash?

            //TODO : Move out of here eventually
            //TODO : add the Transform.Position in the equation (the lower Y = in front?) OR way until there is a Z in Transform.Position

            float stepPerLayer = 256;
            int nbMaxDrawOrder = 100; //Nb of layers
            float min = (MathHelper.Max(0, DrawOrder - 1)*stepPerLayer);
            float max = DrawOrder*stepPerLayer;
            float zIndex = MathHelper.Min(max, min + _zIndex)/(nbMaxDrawOrder*stepPerLayer);

            //Console.WriteLine("[{0}] => zIndex : {1}", GameObject.Transform.Position, zIndex);

            Texture2D texture = Engine.Textures[TextureName];

            Rectangle drawnPortion = _drawnPortion == Rectangle.Empty
                ? new Rectangle(0, 0, texture.Width, texture.Height)
                : _drawnPortion; //If the Portion is not set, create a new rect with the whole texture

            Rectangle posRect = new Rectangle(
                (int) GameObject.Transform.Position.X + (int) Origin.X,
                (int) GameObject.Transform.Position.Y + (int) Origin.Y,
                Width,
                Height);

            spriteBatch.Draw(
                texture,
                posRect,
                drawnPortion,
                _drawingColor,
                GameObject.Transform.Rotation,
                _origin,
                _spriteEffects,
                zIndex);

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
        //    //z = 1.0f - ((float)(GameObject.Transform.Position.Y + Height) (float)(TileMap.Height * Engine.TileHeight));

        //    z += _zDelta;
        //    ZIndex = Math.Min(z, 0.999f);
        //}
    }
}