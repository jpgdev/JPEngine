using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public class FormWindowManagerTests : WindowManagerTests
    {
        private Form _form;

        [TestFixtureSetUp]
        public override void SetUpTest()
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
        public override void WindowResizeTest()
        {
            const int height = 123;
            const int width = 456;

            Engine.Window.Height = height;
            Assert.AreEqual(Engine.Window.Height, height);
            
            Engine.Window.Width = width;
            Assert.AreEqual(Engine.Window.Width , width);
        }

        [Test]
        public override void WindowFullScreenTest()
        {
            Engine.Window.IsFullScreen = true;
            Assert.True(Engine.Window.IsFullScreen);
            Assert.AreEqual(_form.WindowState, FormWindowState.Maximized);
        }
    }
}
