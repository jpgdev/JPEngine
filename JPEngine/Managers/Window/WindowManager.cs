using System;
using System.Windows.Forms;
using JPEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    //TODO: Rename this to Renderer? or something like that
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

        #endregion

        internal WindowManager(IGraphicsDeviceService graphicsDeviceService)
        {
            if (graphicsDeviceService == null)
                throw new ArgumentNullException("The graphicsDeviceService cannot be null.");

            GraphicsDeviceService = graphicsDeviceService;
        }

        public abstract void ApplySettings();

        #region WindowManager Factory

        internal static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
        {
            if (graphicsDeviceService is GraphicsDeviceManager)
                return Create(graphicsDeviceService as GraphicsDeviceManager);

            var form = gameWindow.GetForm();
            if (form == null)
                throw new ArgumentException("The game window could not be found.");

            return new OpenTKWindowManager(graphicsDeviceService as GraphicsDeviceService, form);
        }

        internal static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, IntPtr windowHandle)
        {
            if (graphicsDeviceService is GraphicsDeviceManager)
                return Create(graphicsDeviceService as GraphicsDeviceManager);

            var window = Control.FromHandle(windowHandle) as Form;
            if (window == null)
                throw new ArgumentException("The windowHandle is not a valid Form handle.");

            return new FormWindowManager(graphicsDeviceService, window);
        }

        internal static WindowManager Create(GraphicsDeviceManager graphicsDeviceService)
        {
            return new BasicWindowManager(graphicsDeviceService);
        }

        #endregion
    }
}