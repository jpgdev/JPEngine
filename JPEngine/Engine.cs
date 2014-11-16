#region Include Statements

using System;
using JPEngine.Entities;
using JPEngine.Managers;
using JPEngine.Utils.ScriptConsole;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace JPEngine
{
    public static class Engine
    {
        #region Attributes
        
        private static ContentManager _contentManager;

        private static ScriptConsole _console;

        //Managers
        private static SpriteBatchManager _spriteBatchManager;
        private static WindowManager _windowManager;
        private static EntitiesManager _entityManager;
        private static InputManager _inputManager;
        private static SettingsManager _settingsManager;
        private static CameraManager _cameraManager;

        //Resources managers
        private static TextureManager _textureManager;
        private static MusicManager _musicManager;
        private static SoundFXManager _soundManager;
        private static FontsManager _fontsManager;

        #endregion

        #region Properties

        public static ScriptConsole Console
        {
            get { return _console; }
            set { _console = value; }
        }

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

        public static EntitiesManager Entities
        {
            get { return _entityManager; }
        }

        public static CameraManager Cameras
        {
            get { return _cameraManager; }
        }

        public static FontsManager Fonts
        {
            get { return _fontsManager; }
        }

        #endregion
        
        #region Initialization

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="game">The Form containing the game handle.</param>
        //public static void Initialize(Game game)
        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager, Game game)
        {
            _windowManager = WindowManager.Create(graphicsDeviceManager, game);

            InitializeCore(graphicsDeviceManager);
        }

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceService"></param>
        /// <param name="windowHandle">The Form containing the game handle.</param>
        //public static void Initialize(Game game)
        public static void Initialize(IGraphicsDeviceService graphicsDeviceService, IntPtr windowHandle)
        {
            _windowManager = WindowManager.Create(graphicsDeviceService, windowHandle);

            InitializeCore(graphicsDeviceService);
        }

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceService"></param>
        /// <param name="gameWindow">The Game window.</param>
        //public static void Initialize(Game game)
        public static void Initialize(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
        {
            _windowManager = WindowManager.Create(graphicsDeviceService, gameWindow);

            InitializeCore(graphicsDeviceService);
        }

        private static void InitializeCore(IGraphicsDeviceService graphicsDeviceService)
        {
            if (graphicsDeviceService == null)
                throw new NullReferenceException("The graphicsDeviceService cannot be null.");

            //TODO: Keep globally and use services?
            var services = new GameServiceContainer();
            services.AddService(typeof (IGraphicsDeviceService), graphicsDeviceService);

            //TODO: Enable the user to change the RootDirectory
            //TODO: EngineSettings object?
            _contentManager = new ContentManager(services, "Content");

            _spriteBatchManager = new SpriteBatchManager(_windowManager.GraphicsDevice);

            _entityManager = new EntitiesManager();
            _settingsManager = new SettingsManager();
            _inputManager = new InputManager();
            _cameraManager = new CameraManager();
            _musicManager = new MusicManager(_contentManager);
            _soundManager = new SoundFXManager(_contentManager);
            _textureManager = new TextureManager(_contentManager);
            _fontsManager = new FontsManager(_contentManager);

            _spriteBatchManager.Initialize();
            _windowManager.Initialize();
            _entityManager.Initialize();
            _settingsManager.Initialize();
            _inputManager.Initialize();
            _cameraManager.Initialize();
            _musicManager.Initialize();
            _soundManager.Initialize();
            _textureManager.Initialize();
            _fontsManager.Initialize();
        }

        #endregion

        #region Methods

        public static void UnloadContent()
        {
            _musicManager.UnloadContent();
            _soundManager.UnloadContent();
            _textureManager.UnloadContent();

            _contentManager.Unload();
        }

        public static void Update(GameTime gameTime)
        {
            _spriteBatchManager.Update(gameTime);
            _inputManager.Update(gameTime);

            if (!(_console != null &&
                  _console.IsActive &&
                  _console.Options.PauseGameWhenOpened))
            {
                _entityManager.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime)
        {
            _windowManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Better version that wraps and manage the layers, z-index etc...
            SpriteBatch spriteBatch = _spriteBatchManager.Begin();

            _entityManager.Draw(spriteBatch, gameTime);

            _spriteBatchManager.End();

            if (_console != null)
                _console.Draw(spriteBatch, gameTime);
        }

        #endregion
    }
}