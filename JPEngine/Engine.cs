#region Include Statements

using System;
using JPEngine.ECS;
using JPEngine.Managers;
using JPEngine.Utils;
using JPEngine.Utils.ScriptConsole;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace JPEngine
{
    //TODO: Inherit from DrawableGameComponent to be sure we will be called?


    public static class Engine
    {
        #region Attributes

        private static GraphicsDeviceManager _graphicsDeviceManager;
        private static Game _game;
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

        public static EntitiesManager Entities
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

            //SpriteBatch spriteBatch = new SpriteBatch(_graphicsDeviceManager.GraphicsDevice);

            _spriteBatchManager = new SpriteBatchManager(_graphicsDeviceManager.GraphicsDevice);
            _windowManager = new WindowManager(_graphicsDeviceManager);
            _entityManager = new EntitiesManager();
            _settingsManager = new SettingsManager();
            _inputManager = new InputManager();
            _cameraManager = new CameraManager();
            _musicManager = new MusicManager(game.Content);
            _soundManager = new SoundFXManager(game.Content);
            _textureManager = new TextureManager(game.Content);
            
            _spriteBatchManager.Initialize();
            _windowManager.Initialize();
            _entityManager.Initialize();
            _settingsManager.Initialize();
            _inputManager.Initialize();
            _cameraManager.Initialize();
            _musicManager.Initialize();
            _soundManager.Initialize();
            _textureManager.Initialize();



            //TODO: Add a way to load the resource directly from the Engine NOT the game
            //TODO: Implement a BitmapFont?
            _console = new ScriptConsole(game, game.Content.Load<SpriteFont>("Fonts/ConsoleFont"))
            {
                ToggleKey = Keys.F1
            };

            _console.Initialize();
        }

        public static void UnloadContent()
        {
            _musicManager.UnloadContent();
            _soundManager.UnloadContent();
            _textureManager.UnloadContent();
        }

        public static void Update(GameTime gameTime)
        {
            _spriteBatchManager.Update(gameTime);

            if(!_console.IsActive)
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

            _console.Draw(spriteBatch, gameTime);
        }

        #endregion
    }
}