using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public abstract class WindowManagerTests
    {
        protected GraphicsDeviceService _graphicsDeviceService;

        [TestFixtureSetUp]
        public abstract void SetUpTest();

        [Test]
        public abstract void WindowResizeTest();

        [Test]
        public abstract void WindowFullScreenTest();
        
        [TestFixtureTearDown]
        public void Finished()
        {
            Engine.UnloadContent();
            _graphicsDeviceService.GraphicsDevice.Dispose();
            _graphicsDeviceService.Release(true);
        }
    }
}
