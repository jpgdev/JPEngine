using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;

namespace UnitTestProject.ManualTests
{
    /// <summary>
    /// A test that is validated with a "human presence".
    /// </summary>
    public static class ManualTestsCore
    {
        
        public static void FormFullscreenTest()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form _form = new Form()
            {
                Width = 800,
                Height = 600
            };
            //_form.Show();
            GraphicsDeviceService _graphicsDeviceService = GraphicsDeviceService.AddRef(_form.Handle, _form.ClientSize.Width,
                _form.ClientSize.Height);

            Engine.Initialize(_graphicsDeviceService, _form.Handle);
            //Engine.Window.IsFullScreen = true;

            Timer t = new Timer();
            t.Interval = 3000;
            t.Tick += (sender, args) => { Engine.Window.IsFullScreen = !Engine.Window.IsFullScreen; };
            t.Start();


            Application.Run(_form);
        }

    }
}
