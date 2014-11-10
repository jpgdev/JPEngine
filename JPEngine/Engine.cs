#region Include Statements

using System;
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

        private static GraphicsDeviceManager _graphicsDeviceManager;
        private static Game _game;

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

        public static WindowManager Window
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
        /// <param name="game"></param>
        public static void Initialize(Game game)
        {
            if(game == null)
                throw new NullReferenceException("The game cannot be null.");

            _game = game;

            //Setup the GraphicsDeviceManager
            var graphicsService = (IGraphicsDeviceService) game.Services.GetService(typeof(IGraphicsDeviceService));
            _graphicsDeviceManager = graphicsService as GraphicsDeviceManager ?? new GraphicsDeviceManager(_game);
            
            _spriteBatchManager = new SpriteBatchManager(_graphicsDeviceManager.GraphicsDevice);
            _spriteBatchManager.Initialize();

            _windowManager = new WindowManager(_graphicsDeviceManager);
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
            _musicManager = new MusicManager(game.Content);
            _musicManager.Initialize();

            _soundManager = new SoundFXManager(game.Content);
            _soundManager.Initialize();

            _textureManager = new TextureManager(game.Content);
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
            _graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Better version that wraps and manage the layers, z-index etc...
            SpriteBatch spriteBatch = _spriteBatchManager.Begin();

            _entityManager.Draw(spriteBatch, gameTime);

            _spriteBatchManager.End();
        }

        #endregion
    }
}