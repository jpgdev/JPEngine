﻿using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class FormWindowManager : WindowManager
    {
        //TODO : Test this class

        private readonly Form _windowForm;

        private FormBorderStyle _defaultBorderStyle;

        public override bool IsFullScreen
        {
            get { return _windowForm.WindowState == FormWindowState.Maximized; }
            set
            {
                //If the window was not FullScreen, and is now changing to FullScreen
                if (value && !IsFullScreen)
                    _defaultBorderStyle = _windowForm.FormBorderStyle;
                    
                _windowForm.FormBorderStyle = value ? FormBorderStyle.None : _defaultBorderStyle;
                _windowForm.WindowState = value ? FormWindowState.Maximized : FormWindowState.Normal;
                ApplySettings();
            }
        }

        public FormWindowManager(IGraphicsDeviceService graphicsDeviceService, Form windowForm) 
            : base(graphicsDeviceService)
        {
            _windowForm = windowForm;
            _defaultBorderStyle = _windowForm.FormBorderStyle;
        }

        public override void ApplySettings()
        {
            _windowForm.Width = Width;
            _windowForm.Height = Height;
        }
    }
}