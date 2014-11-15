using System;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;

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
