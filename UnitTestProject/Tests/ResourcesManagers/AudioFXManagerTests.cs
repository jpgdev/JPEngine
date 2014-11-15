using System.Collections.Generic;
using System.Windows.Forms;
using JPEngine;
using JPEngine.Utils;
using NUnit.Framework;

namespace UnitTestProject.Tests.ResourcesManagers
{
    
    [TestFixture]
    public class AudioFXManagerTests
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
        public void TestSoundFXLoad()
        {
            const string name = "ammo_pickup";
            const string path = "Sounds/ammo_pickup";

            Assert.IsTrue(Engine.SoundFX.Add(name, path, true));
            Assert.IsTrue(Engine.SoundFX.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.SoundFX.IsResourceLoaded(name));
            Assert.NotNull(Engine.SoundFX[name]);
        }

        [Test]
        public void TestSoundFXUnloadContent()
        {
            const string name = "ammo_pickup";
            const string path = "Sounds/ammo_pickup";

            Assert.IsTrue(Engine.SoundFX.Add(name, path, true));

            Engine.SoundFX.UnloadContent();

            Assert.AreEqual(Engine.SoundFX.AmountAdded, 0);
            Assert.AreEqual(Engine.SoundFX.AmountLoaded, 0);
            
            Assert.Catch<KeyNotFoundException>(() => Engine.SoundFX.GetResource(name));
        }

        [Test]
        public void TestSoundFX_Play()
        {
            const string name = "ammo_pickup";
            const string path = "Sounds/ammo_pickup";

            Assert.IsTrue(Engine.SoundFX.Add(name, path, true));

            Assert.IsTrue(Engine.SoundFX[name].Play(0f, 0, 0));
        }

        [TearDown]
        public void UnloadContent()
        {
            Engine.SoundFX.UnloadContent();
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
