using System;
using JPEngine.Entities;
using JPEngine.Events;
using Microsoft.Xna.Framework;

namespace JPEngine.Components
{
    public class BaseComponent : IUpdateableComponent, ICloneable
    {
        #region Attributes

        private readonly Entity _gameObject;
        private bool _enabled = true;
        private bool _started = false;
        private string _tag = string.Empty;
        private int _updateOrder;

        #endregion

        #region Properties

        public Entity GameObject
        {
            get { return _gameObject; }
        }

        public string Tag
        {
            get { return _tag; }
            protected set
            {
                if (_tag == value) 
                    return;
                
                string oldValue = _tag;
                _tag = value;
                if(TagChanged != null)
                    TagChanged(this, new ValueChangedEventArgs<string>(oldValue, _tag));
            }
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

        public bool Started
        {
            get { return _started; }
        }

        /// <summary>
        ///     The order in which the component will be updated. 0 = first, Int.MaxValue = last.
        /// </summary>
        public int UpdateOrder
        {
            get { return _updateOrder; }
            set
            {
                if (_updateOrder == value) 
                    return;

                int oldValue = _updateOrder;
                _updateOrder = value;
                if (UpdateOrderChanged != null)
                    UpdateOrderChanged(this, new ValueChangedEventArgs<int>(oldValue, _updateOrder));
            }
        }

        #endregion

        protected BaseComponent(Entity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            _gameObject = entity;

            EnabledChanged += OnEnabledChanged;
            UpdateOrderChanged += OnUpdateOrderChanged;
            UpdateOrderChanged += OnTagChanged;
        }

        public virtual void Initialize()
        {
        }

        public void Start()
        {
            if (_started)
                return;
            
            StartCore();
            _started = true;
        }

        protected virtual void StartCore()
        {
        }

        public event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;
        public event EventHandler<ValueChangedEventArgs<int>> UpdateOrderChanged;
        public event EventHandler<ValueChangedEventArgs<string>> TagChanged;

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

        public virtual void OnTagChanged(object sender, EventArgs e)
        {
        }
        
        #endregion

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }
    }
}