using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.Tests.ResourcesManagers
{

    [TestFixture]
    public class MusicManagerTests
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

        //TODO: Uncomment and fill this
        //[Test]
        //public void TestMusicLoad()
        //{
        //    const string name = "ammo_pickup";

        //    Engine.Music.Add(name, "Sounds/ammo_pickup", true);
        //    Assert.IsTrue(Engine.Music.IsResourcePathAdded(name));
        //    Assert.IsTrue(Engine.Music.IsResourceLoaded(name));
        //    Assert.NotNull(Engine.Music[name]);
        //}


        //[Test]
        //public void TestSoundFXUnloadContent()
        //{
        //    const string name = "ammo_pickup";
        //    const string path = "Sounds/ammo_pickup";

        //    Assert.IsTrue(Engine.Music.Add(name, path, true));

        //    Engine.Music.UnloadContent();

        //    Assert.AreEqual(Engine.Music.AmountAdded, 0);
        //    Assert.AreEqual(Engine.Music.AmountLoaded, 0);

        //    Assert.Catch<KeyNotFoundException>(() => Engine.Music.GetResource(name));
        //}


        //[Test]
        //public void TestSoundFX_Play()
        //{
        //    const string name = "ammo_pickup";
        //    const string path = "Sounds/ammo_pickup";

        //    Assert.IsTrue(Engine.Music.Add(name, path, true));

        //    Assert.IsTrue(Engine.Music[name].Play());
        //}

        //[TearDown]
        //public void UnloadContent()
        //{
        //    Engine.Music.UnloadContent();
        //}

        [TestFixtureTearDown]
        public void Finished()
        {
            Engine.UnloadContent();
            _graphicsDeviceService.GraphicsDevice.Dispose();
            _graphicsDeviceService.Release(true);
        }
    }
}
