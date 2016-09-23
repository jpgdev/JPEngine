#region Include Statements

using System;
using System.Collections.Generic;
using JPEngine.Entities;
using JPEngine.Graphics;
using JPEngine.Managers;
using JPEngine.Managers.Input;
using JPEngine.Managers.Window;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

#endregion

namespace JPEngine
{
    public static class Engine
    {
        #region Attributes


        //TODO: Improve this, make a class to add them (like the Service.AddService) and split them in here (2 lists : IRenderableManagers & IUpdateableManager)
        private static Dictionary<Type, IManager> _customManagers;

        //TODO: Make Interface for each managers to abstract the functionalities

        //TODO: Remove this, since it may not be used in custom ResourceManagers
        private static ContentManager _contentManager;

        //TODO: Use with some kind of GameManager/GamePlayManager?
        //TODO: Remove this from the Engine, the can use it or not, do not force it
        private static EntitiesManager _entitiesManager;

        private static ISpriteRenderer<Texture2D, SpriteFont> _spriteRenderer;
        private static IWindowManager _windowManager;
        private static IResourceManager<Texture2D> _texturesManager;
        private static IResourceManager<Song> _musicManager;
        private static IResourceManager<SoundEffect> _soundFXManager;
        private static IResourceManager<SpriteFont> _fontsManager;
        private static ISettingsManager _settingsManager;
        private static IInputManager _inputManager;
        private static ICameraManager _cameraManager;

        #endregion

        #region Properties

        public static Dictionary<Type, IManager> Managers
        {
            get { return _customManagers; }
            private set
            {
                if (_customManagers != null)
                    foreach (IManager manager in _customManagers.Values)
                        manager.Dispose();

                _customManagers = value;
            }
        }

        public static ISpriteRenderer<Texture2D, SpriteFont> SpriteRenderer
        {
            get { return _spriteRenderer; }
            private set
            {
                if (_spriteRenderer != null)
                    _spriteRenderer.Dispose();

                _spriteRenderer = value;
            }
        }

        public static EntitiesManager Entities
        {
            get { return _entitiesManager; }
            private set
            {
                if (_entitiesManager != null)
                    _entitiesManager.Dispose();

                _entitiesManager = value;
            }
        }

        public static IWindowManager Window
        {
            get { return _windowManager; }
            private set
            {
                if(_windowManager != null)
                    _windowManager.Dispose();

                _windowManager = value;
            }
        }

        public static IResourceManager<Texture2D> Textures
        {
            get { return _texturesManager; }
            private set
            {
                if (_texturesManager != null)
                    _texturesManager.Dispose();

                _texturesManager = value;
            }
        }

        public static IResourceManager<Song> Music
        {
            get { return _musicManager; }
            private set
            {
                if (_musicManager != null)
                    _musicManager.Dispose();

                _musicManager = value;
            }
        }

        public static IResourceManager<SoundEffect> SoundFX
        {
            get { return _soundFXManager; }
            private set
            {
                if (_soundFXManager != null)
                    _soundFXManager.Dispose();

                _soundFXManager = value;
            }
        }

        public static IResourceManager<SpriteFont> Fonts
        {
            get { return _fontsManager; }
            private set
            {
                if (_fontsManager != null)
                    _fontsManager.Dispose();

                _fontsManager = value;
            }
        }

        public static ISettingsManager Settings
        {
            get { return _settingsManager; }
            private set
            {
                if (_settingsManager != null)
                    _settingsManager.Dispose();

                _settingsManager = value;
            }
        }

        public static IInputManager Input
        {
            get { return _inputManager; }
            private set
            {
                if (_inputManager != null)
                    _inputManager.Dispose();

                _inputManager = value;
            }
        }

        public static ICameraManager Cameras
        {
            get { return _cameraManager; }
            private set
            {
                if (_cameraManager != null)
                    _cameraManager.Dispose();

                _cameraManager = value;
            }
        }

        public static float FramesPerSecond { get; private set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="game">The game.</param>
        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager, Game game)
        {
            Window = WindowManagerFactory.Create(graphicsDeviceManager, game);
            InitializeDefaultCore(graphicsDeviceManager);
        }

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceService"></param>
        /// <param name="windowHandle">The Form containing the game handle.</param>
        public static void Initialize(IGraphicsDeviceService graphicsDeviceService, IntPtr windowHandle)
        {
            Window = WindowManagerFactory.Create(graphicsDeviceService, windowHandle);
            InitializeDefaultCore(graphicsDeviceService);
        }

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceService"></param>
        /// <param name="gameWindow">The Game window.</param>
        public static void Initialize(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
        {
            Window = WindowManagerFactory.Create(graphicsDeviceService, gameWindow);
            InitializeDefaultCore(graphicsDeviceService);
        }

        /// <summary>
        /// Initialize the engine.
        /// </summary>
        /// <param name="graphicsDeviceService"></param>
        /// <param name="windowManager">The window manager.</param>
        public static void Initialize(IGraphicsDeviceService graphicsDeviceService, IWindowManager windowManager)
        {
            if(windowManager == null)
                throw new ArgumentNullException("windowManager");

            Window = windowManager;

            InitializeDefaultCore(graphicsDeviceService);
        }

        public static void Initialize(
            IWindowManager windowManager,
            IInputManager inputManager,
            IResourceManager<Texture2D> textureManager,
            IResourceManager<Song> musicManager,
            IResourceManager<SoundEffect> soundFXManager,
            IResourceManager<SpriteFont> fontsManager,
            ISettingsManager settingsManager,
            ICameraManager cameraManager)
        {
            //TODO: Do not require EVERYTHING... Maybe only put the Managers setter public?

            if(windowManager == null)
                throw new ArgumentNullException("windowManager");

            if (inputManager == null)
                throw new ArgumentNullException("inputManager");

            if (textureManager == null)
                throw new ArgumentNullException("textureManager");

            if (musicManager == null)
                throw new ArgumentNullException("musicManager");

            if (soundFXManager == null)
                throw new ArgumentNullException("soundFXManager");

            if (fontsManager == null)
                throw new ArgumentNullException("fontsManager");

            if (settingsManager == null)
                throw new ArgumentNullException("settingsManager");

            if (cameraManager == null)
                throw new ArgumentNullException("cameraManager");

            Window = windowManager;
            Input = inputManager;
            Textures = textureManager;
            Music = musicManager;
            SoundFX = soundFXManager;
            Fonts = fontsManager;
            Settings = settingsManager;
            Cameras = cameraManager;

            /////////////////////
            // TODO: FOR NOW
            Entities = new EntitiesManager();
            SpriteRenderer = new SpriteBatchRenderer(Window.GraphicsDevice);
            ////////////////////

            InitializeManagers();
        }

        private static void InitializeDefaultCore(IGraphicsDeviceService graphicsDeviceService)
        {
            if (graphicsDeviceService == null)
                throw new ArgumentNullException("graphicsDeviceService");

            //TODO: Remove this or make a way to remove it if it is not used (ContentManager)
            ////////////////////////////////////////////////////////////////
            var services = new GameServiceContainer();
            services.AddService(typeof (IGraphicsDeviceService), graphicsDeviceService);

            //TODO: Enable the user to change the RootDirectory
            _contentManager = new ContentManager(services, "Content");
            ///////////////////////////////////////////////////

            SpriteRenderer = new SpriteBatchRenderer(Window.GraphicsDevice);
            Entities = new EntitiesManager();
            Settings = new SettingsManager();
            Input = new InputManager();
            Cameras = new CameraManager();
            Music = new MusicManager(_contentManager);
            SoundFX = new SoundFXManager(_contentManager);
            Textures = new TextureManager(_contentManager);
            Fonts = new FontsManager(_contentManager);

            InitializeManagers();
        }

        private static void InitializeManagers()
        {
            Managers = new Dictionary<Type, IManager>();

            SpriteRenderer.Initialize();
            Window.Initialize();
            Entities.Initialize();
            Settings.Initialize();
            Input.Initialize();
            Cameras.Initialize();
            Music.Initialize();
            SoundFX.Initialize();
            Textures.Initialize();
            Fonts.Initialize();
        }

        #endregion

        #region Methods

        public static void UnloadContent()
        {
            Music.UnloadContent();
            SoundFX.UnloadContent();
            Textures.UnloadContent();

            if (_contentManager != null)
                _contentManager.Unload();
        }

        public static void Update(GameTime gameTime)
        {
            Input.Update(gameTime);

            foreach (IManager manager in _customManagers.Values)
            {
                IUpdateableManager updateableManager = manager as IUpdateableManager;
                if (updateableManager != null)
                    updateableManager.Update(gameTime);
            }

            //TODO: Remove from the engine
            Entities.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            Window.GraphicsDevice.Clear(Color.CornflowerBlue);

            FramesPerSecond = 1f/ (float)gameTime.ElapsedGameTime.TotalSeconds;

            Console.WriteLine("FPS: " + FramesPerSecond);

            //TODO: Better version that wraps and manage the layers, z-index etc...

            //TODO: Remove from the engine
            /////////////////////////////////////////////////
            Entities.Draw(gameTime);

//          SpriteRenderer.End();

            /////////////////////////////////////////////////

            foreach (IManager manager in _customManagers.Values)
            {
                IRenderableManager renderableManager = manager as IRenderableManager;
                if(renderableManager != null)
                    renderableManager.Draw(gameTime);
            }
        }

        #endregion
    }
}
