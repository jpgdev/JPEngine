using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    public class WindowManager : Manager
    {


        //TODO: Delete this Manager and make the GraphicsDeviceManager public in the Engine directly

#region Attributes

        private GraphicsDeviceManager _graphicsManager;

        private int _screenWidth;
        private int _screenHeight;
        private bool _isFullScreen;

#endregion

#region Properties

        public int ScreenWidth
        {
            get { return _screenWidth; }
            set 
            {               
                _screenWidth = value;
                //ApplySettings();
            }
        }

        public int ScreenHeight
        {
            get{ return _screenHeight; }
            set 
            {               
                _screenHeight = value;
                //ApplySettings();
            }
        }

        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set 
            {                
                _isFullScreen = value;
                //ApplySettings();
            }
        }

#endregion

#region Constructors

        internal WindowManager(GraphicsDeviceManager graphicsManager)
            : base()
        {
            _graphicsManager = graphicsManager;
        }

#endregion

#region Methods

        protected override bool InitializeCore()
        {
            _screenHeight = _graphicsManager.GraphicsDevice.Viewport.Height;
            _screenWidth = _graphicsManager.GraphicsDevice.Viewport.Width;
            _isFullScreen = _graphicsManager.IsFullScreen;

            return true;
        }

        public void SetScreenDimensions(int width, int height, bool isFullscreen = false)
        {
            _screenWidth = width;
            _screenHeight = height;
            _isFullScreen = isFullscreen;

            ApplySettings();
        }
        
        public void ApplySettings()
        {
            _graphicsManager.IsFullScreen = _isFullScreen;
            _graphicsManager.PreferredBackBufferHeight = _screenHeight;
            _graphicsManager.PreferredBackBufferWidth = _screenWidth;

            _graphicsManager.ApplyChanges();
        }

#endregion
    }
}
