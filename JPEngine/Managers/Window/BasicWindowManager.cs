using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public class BasicWindowManager : WindowManager
    {
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

        #endregion

        #region Constructors
        
        internal BasicWindowManager(GraphicsDeviceManager graphicsDeviceService)
            :base(graphicsDeviceService)
        {
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