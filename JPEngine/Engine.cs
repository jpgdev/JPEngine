#region Include Statements
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace JPEngine
{   

    public static class Engine
    {

#region Attributes

        //private static Engine _instance;

        private static GraphicsDevice _graphicsDevice;    

        //Managers
        private static SpriteBatchManager _spriteBatchManager;
        private static TextureManager _textureManager;
        private static WindowManager _windowManager;
        private static MusicManager _musicManager;
        private static SoundManager _soundManager;        

#endregion

#region Properties

        public static SpriteBatchManager SpriteManager 
        {
            get { return _spriteBatchManager; } 
        }

        public static WindowManager WindowManager
        {
            get { return _windowManager; }
        }

        public static TextureManager TextureManager
        {
            get { return _textureManager; }
        }

        public static MusicManager MusicManager
        {
            get { return _musicManager; }
        }

        public static SoundManager SoundManager
        {
            get { return _soundManager; }
        } 

#endregion
        
#region Methods

        public static void Initialize(GraphicsDeviceManager graphDeviceManager)
        {
            //_instance = new Engine();

            _graphicsDevice = graphDeviceManager.GraphicsDevice;
            _spriteBatchManager = new SpriteBatchManager(_graphicsDevice);
            _windowManager = new WindowManager(graphDeviceManager);
            
        }

        /// <summary>
        /// Initialize the resource managers.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            _musicManager = new MusicManager(content);
            _soundManager = new SoundManager(content); 
            _textureManager = new TextureManager(content);
        }

        public static void UnloadContent()
        {

        }

        public static void Update(GameTime gameTime)
        {
            _spriteBatchManager.Update(gameTime);
            _musicManager.Update(gameTime);
            _soundManager.Update(gameTime);
        }


        public static void Draw()
        {
            _spriteBatchManager.Draw();
        }

#endregion
       
    }
}
