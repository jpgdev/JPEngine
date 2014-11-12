using System;
using JPEngine.ECS.Components;
using Microsoft.Xna.Framework;

namespace JPEngine.ECS
{
    public class EntityComponent : IEntityComponent, IEntityUpdateable, ICloneable
    {
        private readonly Entity _gameObject;
        private bool _enabled = true;
        private string _tag = string.Empty;
        private int _updateOrder;

        protected EntityComponent(Entity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("The entity cannot be null.");

            _gameObject = entity;

            EnabledChanged += OnEnabledChanged;
            UpdateOrderChanged += OnUpdateOrderChanged;
        }

        public virtual void Initialize() { }

        public virtual void Start() { }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public virtual void Update(GameTime gameTime) { }

        #region GameObject methods Wrapper

        public TransformComponent Transform
        {
            get { return _gameObject.Transform; }
        }

        #endregion

        #region Event Handlers

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

        public string Tag
        {
            get { return _tag; }
            protected set { _tag = value; }
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

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }
    }
}