using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public abstract class Manager : IDisposable, IManager
    {
        private bool _isInitialized;

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

        public void Initialize()
        {
            _isInitialized = InitializeCore();
        }

        protected virtual bool InitializeCore()
        {
            return true;
        }

        internal virtual void Update(GameTime gameTime)
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Dispose()
        {
            UnloadContent();
        }

    }
}