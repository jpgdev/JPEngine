using System;

namespace JPEngine.Managers
{
    public abstract class Manager : IManager
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
             InitializeCore();

            _isInitialized = true;
        }

        protected virtual void InitializeCore()
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