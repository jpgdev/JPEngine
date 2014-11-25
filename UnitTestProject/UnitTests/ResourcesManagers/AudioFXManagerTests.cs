using System.Collections.Generic;
using JPEngine;
using NUnit.Framework;

namespace UnitTestProject.UnitTests.ResourcesManagers
{
    
    [TestFixture]
    public class AudioFXManagerTests : ResourceManagerTests
    {
        [Test]
        public override void ResourceAddTest()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public override void ResourceLoadTest()
        {
            const string name = "ammo_pickup";
            const string path = "Sounds/ammo_pickup";

            Assert.IsTrue(Engine.SoundFX.Add(name, path, true));
            Assert.IsTrue(Engine.SoundFX.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.SoundFX.IsResourceLoaded(name));
            Assert.NotNull(Engine.SoundFX[name]);
        }

        [Test]
        public override void UnloadContentTest()
        {
            const string name = "ammo_pickup";
            const string path = "Sounds/ammo_pickup";

            Assert.IsTrue(Engine.SoundFX.Add(name, path, true));

            Engine.SoundFX.UnloadContent();

            Assert.AreEqual(Engine.SoundFX.Added.Length, 0);
            Assert.AreEqual(Engine.SoundFX.Loaded.Length, 0);
            
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
        public override void TearDown()
        {
            Engine.SoundFX.UnloadContent();
        }
    }
}
