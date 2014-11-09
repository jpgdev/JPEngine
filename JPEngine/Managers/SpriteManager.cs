//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;
//using JPEngine.ECS.Components;


//namespace JPEngine.Managers
//{
//    public class SpriteBatchManager : Manager
//    {
//        private SpriteBatch _spriteBatch;

        

//        internal SpriteBatchManager(GraphicsDevice graphicsDevice)
//            : base()
//        {
//            _spriteBatch = new SpriteBatch(graphicsDevice);
//        }
        
//        internal void Update(GameTime gameTime)
//        {

//        }

//        internal void Draw()
//        {
//            _spriteBatch.Begin();


//            _spriteBatch.End();
//        }

//        //public void Draw(DrawableComponent component, Texture2D texture, Vector2 position)
//        //{
//        //    //TODO: Create a list of Sprites to draw and go trhough them all in the Draw call?

//        //    //TODO: Get correct z-index etc...
//        //    float z_index = 1f;

//        //}
//    }
//}
