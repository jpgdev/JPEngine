using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace JPEngine.Managers
{
    public class SpriteBatchManager : Manager
    {
        private SpriteBatch _spriteBatch;

        internal SpriteBatchManager(GraphicsDevice graphicsDevice)
            : base()
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }
        
        internal void Update(GameTime gameTime)
        {

        }

        internal void Draw()
        {
            _spriteBatch.Begin();




            _spriteBatch.End();
        }
    }
}
