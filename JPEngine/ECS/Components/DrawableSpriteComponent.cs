using JPEngine;
using JPEngine.ECS;
using JPEngine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.ECS.Components
{
    public class DrawableSpriteComponent : DrawableComponent
    {
        private Color _drawingColor = Color.White;
        private Rectangle _drawnPortion; //TODO: Rename?
        private Vector2 _origin = Vector2.Zero; //Todo: Move this to Transform?
        private string _textureName;
        private int _width;
        private int _height;
        private float _zIndex;
        private SpriteEffects _spriteEffects = SpriteEffects.None;
        
#region Properties

        public Color DrawingColor
        {
            get { return _drawingColor; }
            set { _drawingColor = value; }
        }

        /// <summary>
        /// The inner part of the resource to draw. Note : Automatically set to Rectangle(0, 0, Width, Height) in the Start().
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

        public string TextureName
        {
            get { return _textureName; }
            set { _textureName = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        
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
            :base(gameObject)
        {
        }

        public override void Initialize()
        {
 	        base.Initialize();
        }


        public override void Start()
        {
            DrawnPortion = new Rectangle(0, 0, Width, Height);
            
            base.Start();
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //todo: Check if the Texture has been loaded? or let it crash?

            spriteBatch.Draw(
               Engine.Textures[TextureName],
               new Rectangle(
                   (int)GameObject.Transform.Position.X + (int)Origin.X,
                   (int)GameObject.Transform.Position.Y + (int)Origin.Y,
                   Width,
                   Height),
               _drawnPortion,
               _drawingColor,
               GameObject.Transform.Rotation,
               _origin,
               _spriteEffects,
               _zIndex);
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
