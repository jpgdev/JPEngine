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

#region Attributes

        private static Engine _instance;

        private GraphicsDevice _graphicsDevice;    

        //Managers
        private SpriteBatchManager _spriteBatchManager;
        private TextureManager _textureManager;
        private WindowManager _windowManager;
        private MusicManager _musicManager;
        private SoundManager _soundManager;        

#endregion

#region Properties

        public static SpriteBatchManager SpriteManager 
        {
            get { return _instance._spriteBatchManager; } 
        }

        public static WindowManager WindowManager
        {
            get { return _instance._windowManager; }
        }

        public static TextureManager TextureManager
        {
            get { return _instance._textureManager; }
        }

        public static MusicManager MusicManager
        {
            get { return _instance._musicManager; }
        }

        public static SoundManager SoundManager
        {
            get { return _instance._soundManager; }
        } 

#endregion

#region Constructor

        private Engine()
        {

        }

#endregion

#region Methods

        public static void Initialize(GraphicsDeviceManager graphDeviceManager)
        {
            _instance = new Engine();

            _instance._graphicsDevice = new GraphicsDevice();
            _instance._spriteBatchManager = new SpriteBatchManager(_instance._graphicsDevice);
            _instance._windowManager = new WindowManager(graphDeviceManager);
            
        }

        /// <summary>
        /// Initialize the resource managers.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            _instance._musicManager = new MusicManager(content);
            _instance._soundManager = new SoundManager(content); 
            _instance._textureManager = new TextureManager(content);
        }

        public static void Update(GameTime gameTime)
        {
            _instance._spriteBatchManager.Update(gameTime);
            _instance._musicManager.Update(gameTime);
            _instance._soundManager.Update(gameTime);
        }


        public static void Draw()
        {
            _instance._spriteBatchManager.Draw();
        }



#endregion
  


        
    }
}
