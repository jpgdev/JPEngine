using System.Collections.Generic;
using System.Windows.Forms;
using JPEngine;
using JPEngine.Utils;
using NUnit.Framework;

namespace UnitTestProject.Tests.ResourcesManagers
{
    [TestFixture]
    public class TextureManagerTests
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
        public void TestTextureLoad()
        {
            const string name = "crate";
            const string path = "Sprites/crate";

            Assert.IsTrue(Engine.Textures.Add(name, path, true));
            Assert.IsTrue(Engine.Textures.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.Textures.IsResourceLoaded(name));
            Assert.NotNull(Engine.Textures[name]);

            Engine.Textures.UnloadContent();
        }

        [Test]
        public void TestTextureUnloadContent()
        {
            const string name = "crate";
            const string path = "Sprites/crate";

            Assert.IsTrue(Engine.Textures.Add(name, path, true));

            Engine.Textures.UnloadContent();

            Assert.AreEqual(Engine.Textures.AmountAdded, 0);
            Assert.AreEqual(Engine.Textures.AmountLoaded, 0);

            Assert.Catch<KeyNotFoundException>(() => Engine.Textures.GetResource(name));
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
