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
        private Configuration _configuration;

        #endregion

        #region Properties

        internal SpriteManager SpriteManager 
        {
            get { return _spriteManager; } 
        }              

        public Configuration Config
        {
            get { return _configuration; }
            set { _configuration = value; }
        }       

        #endregion

        public Engine()
        {
            
        }


        public void Initialize()
        {
            _graphicsDevice = new GraphicsDevice();
            _spriteManager = new SpriteManager(_graphicsDevice);
            _configuration = new Configuration(1280, 720);
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
