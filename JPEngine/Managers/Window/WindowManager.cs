using System;
using System.Windows.Forms;
using JPEngine.Graphics;
using JPEngine.Utils;
using Microsoft.Xna.Framework;
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


        #region WindowManager Factory

        internal static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
        {
            if (graphicsDeviceService is GraphicsDeviceManager)
                throw new NotImplementedException("This is not implemented. You need to use the Game instead of the GameWindow.");
            
            //Get a OpenTKGameWindow
            var form = gameWindow.GetForm();  
            if (form != null)
                return new OpenTKWindowManager(graphicsDeviceService as GraphicsDeviceService, form);
            
            //Get a Windows.Form
            try
            {
                return Create(graphicsDeviceService, gameWindow.Handle);
            }
            catch (ArgumentException) { }

            //Could not find a window...
            throw new ArgumentException("The game window could not be found."); //TODO: Add a more meaningful message?
        }

        internal static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, IntPtr windowHandle)
        {
            if (graphicsDeviceService is GraphicsDeviceManager)
                throw new NotImplementedException("This is not implemented. You need to use the Game instead of the GameWindow.Handle.");

            //Get a Windows.Form
            var window = Control.FromHandle(windowHandle) as Form;
            if (window == null)
                throw new ArgumentException("The windowHandle is not a valid Windows.Form handle.");

            return new FormWindowManager(graphicsDeviceService, window);
        }

        /// <summary>
        /// Will create the basic WindowManager using the XNA's out-of-the-box GraphicsDeviceManager and Game.
        /// </summary>
        /// <param name="graphicsDeviceManager"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        internal static WindowManager Create(GraphicsDeviceManager graphicsDeviceManager, Game game)
        {
            return new BasicWindowManager(graphicsDeviceManager, game);
        }

        #endregion
    }
}