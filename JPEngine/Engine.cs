#region Include Statements

using JPEngine.ECS;
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

        private static GraphicsDevice _graphicsDevice;

        //Managers
        private static SpriteBatchManager _spriteBatchManager;
        private static WindowManager _windowManager;
        private static EntityManager _entityManager;
        private static InputManager _inputManager;
        private static SettingsManager _settingsManager;
        private static CameraManager _cameraManager;

        //Resources managers
        private static TextureManager _textureManager;
        private static MusicManager _musicManager;
        private static SoundFXManager _soundManager;

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

        public static TextureManager Textures
        {
            get { return _textureManager; }
        }

        public static MusicManager Music
        {
            get { return _musicManager; }
        }

        public static SoundFXManager SoundFX
        {
            get { return _soundManager; }
        }

        public static SettingsManager Settings
        {
            get { return _settingsManager; }
        }

        public static InputManager Input
        {
            get { return _inputManager; }
        }

        public static EntityManager Entities
        {
            get { return _entityManager; }
        }

        public static CameraManager Cameras
        {
            get { return _cameraManager; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the managers.
        /// </summary>
        /// <param name="graphDeviceManager"></param>
        /// <param name="content"></param>
        public static void Initialize(GraphicsDeviceManager graphDeviceManager, ContentManager content)
        {
            _graphicsDevice = graphDeviceManager.GraphicsDevice;

            _spriteBatchManager = new SpriteBatchManager(_graphicsDevice);
            _spriteBatchManager.Initialize();

            _windowManager = new WindowManager(graphDeviceManager);
            _windowManager.Initialize();

            _entityManager = new EntityManager();
            _entityManager.Initialize();

            _settingsManager = new SettingsManager();
            _settingsManager.Initialize();

            _inputManager = new InputManager();
            _inputManager.Initialize();

            _cameraManager = new CameraManager();
            _cameraManager.Initialize();
            
            //Resources managers
            _musicManager = new MusicManager(content);
            _musicManager.Initialize();

            _soundManager = new SoundFXManager(content);
            _soundManager.Initialize();

            _textureManager = new TextureManager(content);
            _textureManager.Initialize();
        }

        ///// <summary>
        /////     Initialize the resources managers.
        ///// </summary>
        ///// <param name="content"></param>
        //public static void LoadContent(ContentManager content)
        //{
        //    //_musicManager = new MusicManager(content);
        //    //_musicManager.Initialize();

        //    //_soundManager = new SoundFXManager(content);
        //    //_soundManager.Initialize();

        //    //_textureManager = new TextureManager(content);
        //    //_textureManager.Initialize();
        //}

        public static void UnloadContent()
        {
            _musicManager.UnloadContent();
            _soundManager.UnloadContent();
            _textureManager.UnloadContent();
        }

        public static void Update(GameTime gameTime)
        {
            _spriteBatchManager.Update(gameTime);
            //_musicManager.Update(gameTime);
            //_soundManager.Update(gameTime);
            _entityManager.Update(gameTime);
            _inputManager.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Better version that wraps and manage the layers, z-index etc...
            SpriteBatch spriteBatch = _spriteBatchManager.Begin();

            _entityManager.Draw(spriteBatch, gameTime);

            _spriteBatchManager.End();
        }

        #endregion
    }
}