using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.UnitTests.ResourcesManagers
{
    [TestFixture]
    public abstract class ResourceManagerTests
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
        public abstract void ResourceAddTest();

        [Test]
        public abstract void ResourceLoadTest();

        [Test]
        public abstract void UnloadContentTest();

        [TearDown]
        public abstract void TearDown();

        [TestFixtureTearDown]
        public void Finished()
        {
            Engine.UnloadContent();
            _graphicsDeviceService.GraphicsDevice.Dispose();
            _graphicsDeviceService.Release(true);
        }
    }
}
