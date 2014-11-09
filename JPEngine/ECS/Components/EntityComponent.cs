using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JPEngine.ECS.Components;

namespace JPEngine.ECS
{
    public abstract class EntityComponent : IEntityComponent, IEntityUpdateable
    {
        private string _name = string.Empty;
        private bool _enabled = true;
        private Entity _gameObject;
        private int _updateOrder;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

#region Properties

        public Entity GameObject { get { return _gameObject; } }

        public string Name 
        { 
            get { return _name; }
            protected set { _name = value; }
        }

        public bool Enabled 
        { 
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    if (EnabledChanged != null)
                        EnabledChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The order in which the component will be updated. 0 = first, Int.MaxValue = last.
        /// </summary>
        public int UpdateOrder {
            get { return _updateOrder; }
            set
            {
                if (_updateOrder != value)
                {
                    _updateOrder = value;
                    if (UpdateOrderChanged != null)
                        UpdateOrderChanged(this, EventArgs.Empty);
                }
            }
        }

#endregion

        public EntityComponent (Entity entity)
        {
            this._gameObject = entity;

            EnabledChanged += OnEnabledChanged;
            UpdateOrderChanged += OnUpdateOrderChanged;
        }

        public virtual void Initialize() { }

        public virtual void Start() { }

        //TODO: Fixed update?
        public virtual void Update(GameTime gameTime) { }

#region Event Handler Methods

        protected virtual void OnEnabledChanged(object sender, EventArgs e) { }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs e) { }

#endregion
        
    }
}
