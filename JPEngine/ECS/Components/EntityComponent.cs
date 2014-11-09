using System;
using JPEngine.ECS.Components;
using Microsoft.Xna.Framework;

namespace JPEngine.ECS
{
    public abstract class EntityComponent : IEntityComponent, IEntityUpdateable
    {
        private readonly Entity _gameObject;
        private bool _enabled = true;
        private string _name = string.Empty;
        private int _updateOrder;

        public EntityComponent(Entity entity)
        {
            _gameObject = entity;

            EnabledChanged += OnEnabledChanged;
            UpdateOrderChanged += OnUpdateOrderChanged;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Start()
        {
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        //TODO: Fixed update?
        public virtual void Update(GameTime gameTime)
        {
        }

        #region Event Handler Methods

        protected virtual void OnEnabledChanged(object sender, EventArgs e)
        {
        }

        protected virtual void OnUpdateOrderChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region Properties

        public Entity GameObject
        {
            get { return _gameObject; }
        }

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
        ///     The order in which the component will be updated. 0 = first, Int.MaxValue = last.
        /// </summary>
        public int UpdateOrder
        {
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
    }
}