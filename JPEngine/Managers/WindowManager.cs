using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    //TODO: Rename this to Renderer? or something like that
    public class WindowManager : Manager
    {
        #region Attributes

        //TODO: Make the GraphicsDevice accessible?
        private readonly GraphicsDeviceManager _graphicsManager;
        
        #endregion

        #region Properties

        public Viewport Viewport
        {
            get { return _graphicsManager.GraphicsDevice.Viewport; }
        }

        public int Width
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
                ApplySettings();
            }
        }

        public int Height
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

                ApplySettings();
            }
        }

        public bool IsFullScreen
        {
            get { return _graphicsManager.IsFullScreen; }
            set
            {
                _graphicsManager.IsFullScreen = value;
                ApplySettings();
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
            
            _graphicsManager.PreferredBackBufferHeight = Height;
            _graphicsManager.PreferredBackBufferWidth = Width;

            _graphicsManager.ApplyChanges();
        }

        //TODO: May be dangerous since they may never be disposed of?
        //TODO: Find another place to access this?
        public Texture2D CreateTexture(int width, int height)
        {
            return new Texture2D(_graphicsManager.GraphicsDevice, width, height);
        }

        #endregion
    }
}