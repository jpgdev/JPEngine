#region Include Statements
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace JPEngine
{
    

    public class Engine
    {
        #region Variables

        private SpriteManager _spriteManager;
        private GraphicsDevice _graphicsDevice;

        #endregion

        #region Properties

        public SpriteManager SpriteManager 
        {
            get { return _spriteManager; } 
        }


        #endregion

        public Engine()
        {
            
        }


        public void Initialize()
        {
            _graphicsDevice = new GraphicsDevice();
            _spriteManager = new SpriteManager(_graphicsDevice);
        }

        public void LoadContent(ContentManager content)
        {
            

        }

        public void Update(GameTime gameTime)
        {
            _spriteManager.Update(gameTime);
        }


        public void Draw()
        {
            _spriteManager.Draw();
        }
    }
}
