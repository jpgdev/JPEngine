﻿using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.Tests.Entities
{

    [TestFixture]
    public class ComponentsTests
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
        public void Test()
        {
            //TODO: Fill this
        }

        //TODO: On TagChanged tests
        [TearDown]
        public void OnTagChangedTest()
        {
           
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
