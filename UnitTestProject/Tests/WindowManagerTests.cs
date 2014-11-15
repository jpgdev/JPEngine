using System.Windows.Forms;
using JPEngine;
using JPEngine.Utils;
using NUnit.Framework;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public class WindowManagerTests
    {
        private Form _form;
        private GraphicsDeviceService _graphicsDeviceService;

        [TestFixtureSetUp]
        public void SetUpTest()
        {
            _form = new Form()
            {
                Width = 800, 
                Height = 600
            };

            _graphicsDeviceService = GraphicsDeviceService.AddRef(_form.Handle, _form.ClientSize.Width, _form.ClientSize.Height);
            
            Engine.Initialize(_graphicsDeviceService, _form.Handle);
        }

        [Test]
        public void WindowResizeTest()
        {
            const int height = 200;
            const int width = 150;

            Engine.Window.Height = height;
            Assert.AreEqual(Engine.Window.Height, height);
            
            Engine.Window.Width = width;
            Assert.AreEqual(Engine.Window.Width , width);
            
            Engine.Window.IsFullScreen = true;
            Assert.True(Engine.Window.IsFullScreen);
            Assert.AreEqual(_form.WindowState, FormWindowState.Maximized);
        }

        [TestFixtureTearDown]
        public void Finished()
        {
            Engine.UnloadContent();
            _graphicsDeviceService.GraphicsDevice.Dispose();
            _graphicsDeviceService.Release(true);
        }
    }
}
