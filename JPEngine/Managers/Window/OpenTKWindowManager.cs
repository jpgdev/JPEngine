using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;
using GameWindow = OpenTK.GameWindow;

namespace JPEngine.Managers
{
    public class OpenTKWindowManager : WindowManager
    {
        //TODO: Test this class

        private readonly GameWindow _gameWindow;

        public override bool IsFullScreen
        {
            get { return _gameWindow.WindowState == WindowState.Fullscreen; }
            set
            {
                _gameWindow.WindowState = value ? WindowState.Fullscreen : WindowState.Normal;
                ApplySettings();
            }
        }

        public override bool IsMouseVisible
        {
            get { return _gameWindow.CursorVisible; }
            set { _gameWindow.CursorVisible = value; }
        }

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    _gameWindow.Bounds.X,
                    _gameWindow.Bounds.Y, 
                    _gameWindow.Bounds.Width,
                    _gameWindow.Bounds.Height);
            }
        }

        internal OpenTKWindowManager(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
            : base(graphicsDeviceService)
        {
            if (gameWindow == null)
                throw new ArgumentNullException("The gameWindow cannot be null.");

            _gameWindow = gameWindow;
        }

        public override void ApplySettings()
        {
            _gameWindow.Width = Width;
            _gameWindow.Height = Height;
            //_gameWindow.SwapBuffers();
        }
    }
}
