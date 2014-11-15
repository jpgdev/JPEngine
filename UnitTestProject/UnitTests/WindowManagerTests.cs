﻿using System.Windows.Forms;
using JPEngine;
using JPEngine.Graphics;
using NUnit.Framework;

namespace UnitTestProject.Tests
{
    [TestFixture]
    public class WindowManagerTests
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
        public void WindowResizeTest()
        {
            const int height = 123;
            const int width = 456;

            Engine.Window.Height = height;
            Assert.AreEqual(Engine.Window.Height, height);
            
            Engine.Window.Width = width;
            Assert.AreEqual(Engine.Window.Width , width);
        }

        [Test]
        public void WindowFullScreenTest()
        {
            Engine.Window.IsFullScreen = true;
            Assert.True(Engine.Window.IsFullScreen);

            //TODO: Test for non-Form WindowManager...

            Assert.AreEqual(_form.WindowState, FormWindowState.Maximized);
        }

        //[Test]
        //public void WindowIsMouseVisibleTest()
        //{
        //    //TODO: Test for all possible WindowManager
        //    //Engine.Window.IsMouseVisible = true;
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
