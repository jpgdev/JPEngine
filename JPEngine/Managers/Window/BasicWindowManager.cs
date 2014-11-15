using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public class BasicWindowManager : WindowManager
    {
        private Game _game;

        #region Properties

        private GraphicsDeviceManager _graphicsDeviceManager
        {
            get { return (GraphicsDeviceService as GraphicsDeviceManager); }
        }

        public override bool IsFullScreen
        {
            get { return _graphicsDeviceManager.IsFullScreen; }
            set
            {
                _graphicsDeviceManager.IsFullScreen = value;
                ApplySettings();
            }
        }

        //TODO: I do not have access to the Game object, nor the window, so I don't know how to handle this
        public override bool IsMouseVisible
        {
            get { return _game.IsMouseVisible; }
            set { _game.IsMouseVisible = value; }
        }

        public override Rectangle Bounds
        {
            get { return _game.Window.ClientBounds; }
        }

        #endregion

        #region Constructors
        
        internal BasicWindowManager(GraphicsDeviceManager graphicsDeviceService, Game game)
            :base(graphicsDeviceService)
        {
            if (game == null)
                throw new ArgumentNullException("The game cannot be null.");

            _game = game;
        }

        #endregion

        #region Methods

        public override void ApplySettings()
        {
            _graphicsDeviceManager.PreferredBackBufferHeight = Height;
            _graphicsDeviceManager.PreferredBackBufferWidth = Width;

            _graphicsDeviceManager.ApplyChanges();
        }

        #endregion
    }
}