using System;
using JPEngine.Utils;

namespace JPEngine.Graphics
{
    public class GameGraphicsDeviceService : GraphicsDeviceService
    {

        //private Form _window;
        //private bool _isFullScreen = false;

        //public bool IsFullScreen
        //{
        //    get { return _isFullScreen; }
        //    set
        //    {
        //        _isFullScreen = value;
        //        _window.WindowState = _isFullScreen ? FormWindowState.Maximized : FormWindowState.Normal;
        //    }
        //}

        public GameGraphicsDeviceService(IntPtr windowHandle, int width, int height) 
            : base(windowHandle, width, height)
        {
           
        }

    }
}
