using System.Collections.Generic;
using System.Windows.Forms;
using JPEngine;
using JPEngine.Utils;
using NUnit.Framework;

namespace UnitTestProject.Tests.ResourcesManagers
{
    
    [TestFixture]
    public class FontsManagerTests
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
        public void TestFontsLoad()
        {
            const string name = "font1";
            const string path = "Fonts/font1";

            Assert.IsTrue(Engine.Fonts.Add(name, path, true));
            Assert.IsTrue(Engine.Fonts.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.Fonts.IsResourceLoaded(name));
            Assert.NotNull(Engine.Fonts[name]);
        }

        [Test]
        public void TestFontsUnloadContent()
        {
            const string name = "font1";
            const string path = "Fonts/font1";

            Assert.IsTrue(Engine.Fonts.Add(name, path, true));

            Engine.Fonts.UnloadContent();

            Assert.AreEqual(Engine.Fonts.AmountAdded, 0);
            Assert.AreEqual(Engine.Fonts.AmountLoaded, 0);

            Assert.Catch<KeyNotFoundException>(() => Engine.Fonts.GetResource(name));
        }

        [TearDown]
        public void UnloadContent()
        {
            Engine.Fonts.UnloadContent();
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
