using JPEngine;
using NUnit.Framework;

namespace UnitTestProject.UnitTests.ResourcesManagers
{

    [TestFixture]
    public class MusicManagerTests : ResourceManagerTests
    {
        [Test]
        public override void ResourceAddTest()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public override void ResourceLoadTest()
        {
            throw new System.NotImplementedException();

            //const string name = "ammo_pickup";

            //Engine.Music.Add(name, "Sounds/ammo_pickup", true);
            //Assert.IsTrue(Engine.Music.IsResourcePathAdded(name));
            //Assert.IsTrue(Engine.Music.IsResourceLoaded(name));
            //Assert.NotNull(Engine.Music[name]);
        }

        [Test]
        public override void UnloadContentTest()
        {
            throw new System.NotImplementedException();

            //const string name = "ammo_pickup";
            //const string path = "Sounds/ammo_pickup";

            //Assert.IsTrue(Engine.Music.Add(name, path, true));

            //Engine.Music.UnloadContent();

            //Assert.AreEqual(Engine.Music.AmountAdded, 0);
            //Assert.AreEqual(Engine.Music.AmountLoaded, 0);

            //Assert.Catch<KeyNotFoundException>(() => Engine.Music.GetResource(name));
        }

        [Test]
        public void TestMusic_Play()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void TestMusic_Stop()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void TestMusic_StopAndPlay()
        {
            throw new System.NotImplementedException();
        }

        [TearDown]
        public override void TearDown()
        {
            Engine.Music.UnloadContent();
        }
    }
}
