using System.Windows.Forms;
using JPEngine;
using JPEngine.Components;
using JPEngine.Entities;
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

            Timer t = new Timer {Interval = 3000};
            t.Tick += (sender, args) => { Engine.Window.IsFullScreen = !Engine.Window.IsFullScreen; };
            t.Start();

            Application.Run(_form);
        }
        
        //public static void CursorTests()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);

        //    Form _form = new Form()
        //    {
        //        Width = 800,
        //        Height = 600
        //    };

        //    //Cursor.Hide();
        //    Cursor.Show();

        //    //_form.Show();
        //    GraphicsDeviceService _graphicsDeviceService = GraphicsDeviceService.AddRef(_form.Handle, _form.ClientSize.Width,
        //        _form.ClientSize.Height);

        //    Engine.Initialize(_graphicsDeviceService, _form.Handle);

        //    string name = "crate";
        //    Engine.Textures.Add(name, "Sprites/crate", true);

        //    //TODO: Need to draw the game....

        //    Entity e = new Entity();
        //    e.AddComponent(new DrawableSpriteComponent(e, Engine.Textures[name]));
        //    Engine.Entities.AddEntity(e);

        //    Application.Run(_form);
        //}
    }
}
