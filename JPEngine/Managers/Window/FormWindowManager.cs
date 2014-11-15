using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class FormWindowManager : WindowManager
    {
        //TODO : Test this class

        private readonly Form _windowForm;

        public override bool IsFullScreen
        {
            get { return _windowForm.WindowState == FormWindowState.Maximized; }
            set
            {
                _windowForm.WindowState = value ? FormWindowState.Maximized : FormWindowState.Normal;
                ApplySettings();
            }
        }

        public FormWindowManager(IGraphicsDeviceService graphicsDeviceService, Form windowForm) 
            : base(graphicsDeviceService)
        {
            _windowForm = windowForm;
        }

        public override void ApplySettings()
        {
            _windowForm.Width = Width;
            _windowForm.Height = Height;
        }
    }
}