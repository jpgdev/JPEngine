﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    public abstract class Manager
    {    
        private bool _isInitialized = false;

        public bool IsInitialized
        {
            get { return _isInitialized; }
            protected set
            {
                _isInitialized = value;
                if (_isInitialized && Initialized != null)
                    Initialized(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> Initialized;

        public Manager()
        {
        }

        internal void Initialize() 
        { 
            _isInitialized = InitializeCore();
        }

        protected virtual bool InitializeCore() { return true; }

        internal virtual void Update(GameTime gameTime) { }

        public virtual void UnloadContent() { }
    }
}
