using System.Collections.Generic;
using JPEngine;
using NUnit.Framework;

namespace UnitTestProject.UnitTests.ResourcesManagers
{
    
    [TestFixture]
    public class FontsManagerTests : ResourceManagerTests
    {
        [Test]
        public override void ResourceAddTest()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public override void ResourceLoadTest()
        {
            const string name = "font1";
            const string path = "Fonts/font1";

            Assert.IsTrue(Engine.Fonts.Add(name, path, true));
            Assert.IsTrue(Engine.Fonts.IsResourcePathAdded(name));
            Assert.IsTrue(Engine.Fonts.IsResourceLoaded(name));
            Assert.NotNull(Engine.Fonts[name]);
        }

        [Test]
        public override void UnloadContentTest()
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
        public override void TearDown()
        {
            Engine.Fonts.UnloadContent();
        }
    }
}
