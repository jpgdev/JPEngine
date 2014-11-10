using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class WindowManager : Manager
    {
        #region Attributes

        private readonly GraphicsDeviceManager _graphicsManager;
        
        #endregion

        #region Properties

        public Viewport Viewport
        {
            get { return _graphicsManager.GraphicsDevice.Viewport; }
        }

        public int ScreenWidth
        {
            get { return _graphicsManager.GraphicsDevice.Viewport.Width; }
            set
            {
                _graphicsManager.GraphicsDevice.Viewport = 
                    new Viewport(
                        _graphicsManager.GraphicsDevice.Viewport.X,
                        _graphicsManager.GraphicsDevice.Viewport.Y,
                        value,
                        _graphicsManager.GraphicsDevice.Viewport.Height);
                //_graphicsManager.GraphicsDevice.Viewport.Width = value;
                //ApplySettings();
            }
        }

        public int ScreenHeight
        {
            get { return _graphicsManager.GraphicsDevice.Viewport.Height; }
            set
            {
                _graphicsManager.GraphicsDevice.Viewport =
                   new Viewport(
                       _graphicsManager.GraphicsDevice.Viewport.X,
                       _graphicsManager.GraphicsDevice.Viewport.Y,
                       _graphicsManager.GraphicsDevice.Viewport.Width,
                       value);

                //ApplySettings();
            }
        }

        public bool IsFullScreen
        {
            get { return _graphicsManager.IsFullScreen; }
            set
            {
                _graphicsManager.IsFullScreen = value;
                //ApplySettings();
            }
        }

        #endregion

        #region Constructors

        internal WindowManager(GraphicsDeviceManager graphicsManager)
        {
            if (graphicsManager == null)
                throw new ArgumentNullException("The GraphicsDeviceManager cannot be null.");

            _graphicsManager = graphicsManager;
        }

        #endregion

        #region Methods

        public void ApplySettings()
        {
            _graphicsManager.ApplyChanges();
        }

        #endregion
    }
}