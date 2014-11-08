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
                UpdateWindow();
                _screenWidth = value; 
            }
        }

        public int ScreenHeight
        {
            get{ return _screenHeight; }
            set 
            {
                UpdateWindow();
                _screenHeight = value; 
            }
        }

        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set 
            {
                UpdateWindow();
                _isFullScreen = value; 
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

        public void Initialize() { }

        public void UpdateWindow()
        {
            _graphicsManager.IsFullScreen = _isFullScreen;
            _graphicsManager.PreferredBackBufferHeight = _screenHeight;
            _graphicsManager.PreferredBackBufferWidth = _screenWidth;

            _graphicsManager.ApplyChanges();
        }

#endregion
    }
}
