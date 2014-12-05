using System;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace JPEngine.Managers
{
    public abstract class WindowManager : Manager, IWindowManager
    {
        #region Attributes

        protected readonly IGraphicsDeviceService GraphicsDeviceService;

        #endregion

        #region Properties

        public GraphicsDevice GraphicsDevice
        {
            get { return GraphicsDeviceService.GraphicsDevice; }
        }

        public Viewport Viewport
        {
            get { return GraphicsDeviceService.GraphicsDevice.Viewport; }
        }

        public virtual int Width
        {
            get { return GraphicsDeviceService.GraphicsDevice.Viewport.Width; }
            set
            {
                GraphicsDeviceService.GraphicsDevice.Viewport =
                    new Viewport(
                        GraphicsDeviceService.GraphicsDevice.Viewport.X,
                        GraphicsDeviceService.GraphicsDevice.Viewport.Y,
                        value,
                        GraphicsDeviceService.GraphicsDevice.Viewport.Height);

                ApplySettings();
            }
        }

        public virtual int Height
        {
            get { return GraphicsDeviceService.GraphicsDevice.Viewport.Height; }
            set
            {
                GraphicsDeviceService.GraphicsDevice.Viewport =
                  new Viewport(
                      GraphicsDeviceService.GraphicsDevice.Viewport.X,
                      GraphicsDeviceService.GraphicsDevice.Viewport.Y,
                      GraphicsDeviceService.GraphicsDevice.Viewport.Width,
                      value);

                ApplySettings();
            }
        }

        public abstract bool IsFullScreen{ get; set; }

        public abstract bool IsMouseVisible{ get; set; }

        public abstract Rectangle Bounds { get; }

        #endregion

        internal WindowManager(IGraphicsDeviceService graphicsDeviceService)
        {
            if (graphicsDeviceService == null)
                throw new ArgumentNullException("graphicsDeviceService");

            GraphicsDeviceService = graphicsDeviceService;
        }


        #region Abstract Methods

        public abstract void ApplySettings();

        #endregion
    }
}