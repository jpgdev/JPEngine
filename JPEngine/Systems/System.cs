using System;
using System.Collections.Generic;
using JPEngine.Components;
using JPEngine.Entities;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine.Systems
{
    public abstract class System : ISystem
    {
        private bool _enabled;
        private bool _initialized;
        private IEnumerable<Type> _typesUsed = new List<Type>();

        #region Properties

        public IEnumerable<Type> TypesUsed
        {
            get { return _typesUsed; } 
            protected set { _typesUsed = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value)
                    return;

                bool oldValue = _enabled;
                _enabled = value;
                if (EnabledChanged != null)
                    EnabledChanged(this, new ValueChangedEventArgs<bool>(oldValue, _enabled));
            }
        }

        public bool Initialized
        {
            get { return _initialized; }
        }

        #endregion
       
        public event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;
        
        public void Initialize()
        {
            if (_initialized)
                return;

            IntializeCore();
            _initialized = true;
        }

        protected abstract void IntializeCore();


        public virtual void OnEntityChange(Entity entity)
        {
            

        }



        public abstract void Update(Dictionary<Type, IEnumerable<IComponent>> components, GameTime gameTime);
    }
}