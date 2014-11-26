using System;
using System.Windows.Forms;
using JPEngine.Graphics;
using JPEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers.Window
{
    public static class WindowManagerFactory
    {
        public static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, GameWindow gameWindow)
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

        public static WindowManager Create(IGraphicsDeviceService graphicsDeviceService, IntPtr windowHandle)
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
        public static WindowManager Create(GraphicsDeviceManager graphicsDeviceManager, Game game)
        {
            return new BasicWindowManager(graphicsDeviceManager, game);
        }
    }
}
