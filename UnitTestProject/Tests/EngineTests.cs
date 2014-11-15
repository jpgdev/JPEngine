using System.Windows.Forms;
using JPEngine;
using JPEngine.Utils;
using NUnit.Framework;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public class EngineTests
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

        //[SetUp]
        //public void SetUp()
        //{
        //    //AssemblyUtilities.SetEntryAssembly();
        //}

        [Test]
        public void InitEngineTest()
        {
            Assert.True(Engine.Cameras != null);
            Assert.True(Engine.Cameras.IsInitialized);

            Assert.True(Engine.Entities != null);
            Assert.True(Engine.Entities.IsInitialized);

            Assert.True(Engine.Input != null);
            Assert.True(Engine.Input.IsInitialized);

            Assert.True(Engine.Music != null);
            Assert.True(Engine.Music.IsInitialized);

            Assert.True(Engine.Settings != null);
            Assert.True(Engine.Settings.IsInitialized);

            Assert.True(Engine.SoundFX != null);
            Assert.True(Engine.SoundFX.IsInitialized);

            Assert.True(Engine.SpriteManager != null);
            Assert.True(Engine.SpriteManager.IsInitialized);

            Assert.True(Engine.Textures != null);
            Assert.True(Engine.Textures.IsInitialized);

            Assert.True(Engine.Window != null);
            Assert.True(Engine.Window.IsInitialized);
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
