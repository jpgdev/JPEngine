using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace JPEngine.Managers
{
    class SpriteManager
    {
        SpriteBatch _spriteBatch;

        public SpriteManager(GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }



        public void Update(GameTime gameTime)
        {

        }

        public void Draw()
        {

        }
    }
}
