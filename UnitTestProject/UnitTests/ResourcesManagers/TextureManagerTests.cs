using System;
using System.Collections.Generic;
using JPEngine;
using NUnit.Framework;

namespace UnitTestProject.UnitTests.ResourcesManagers
{
    [TestFixture]
    public class TextureManagerTests : ResourceManagerTests
    {
        [Test]
        public override void ResourceAddTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public override void ResourceLoadTest()
        {
            const string name = "crate";
            const string path = "Sprites/crate";

            Assert.IsTrue(Engine.Textures.Add(name, path, true));
            Assert.IsTrue(Engine.Textures.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.Textures.IsResourceLoaded(name));
            Assert.NotNull(Engine.Textures[name]);
        }
        [Test]
        public override void UnloadContentTest()
        {
            const string name = "crate";
            const string path = "Sprites/crate";

            Assert.IsTrue(Engine.Textures.Add(name, path, true));

            Engine.Textures.UnloadContent();

            Assert.AreEqual(Engine.Textures.AmountAdded, 0);
            Assert.AreEqual(Engine.Textures.AmountLoaded, 0);

            Assert.Catch<KeyNotFoundException>(() => Engine.Textures.GetResource(name));
        }

        [TearDown]
        public override void TearDown()
        {
            Engine.Textures.UnloadContent();
        }
    }
}
