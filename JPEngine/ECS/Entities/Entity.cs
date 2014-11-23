using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Components;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Entities
{
    //Inspired from : https://xnaentitycomponents.codeplex.com/

    public sealed class Entity : ICloneable
    {
        private readonly List<IComponent> _components = new List<IComponent>();
        private readonly List<IDrawableComponent> _drawableComponents = new List<IDrawableComponent>();
        private readonly List<IUpdateableComponent> _updateableComponents = new List<IUpdateableComponent>();

        private readonly Dictionary<string, IComponent> _taggedComponents = new Dictionary<string, IComponent>();

        //The components on which the Drawing and Updating is performed on
        private readonly List<IComponent> _tempComponents = new List<IComponent>();
        private readonly List<IDrawableComponent> _tempDrawableComponents = new List<IDrawableComponent>();
        private readonly List<IUpdateableComponent> _tempUpdateableComponents = new List<IUpdateableComponent>();

        private readonly TransformComponent _transform;

        private bool _initialized;
        private bool _isEnabled;
        private string _tag = string.Empty;
        private readonly long _id;

        public event EventHandler<ValueChangedEventArgs<bool>> EnabledChanged;
        public event EventHandler<ValueChangedEventArgs<string>> TagChanged;

        #region Properties

        public TransformComponent Transform
        {
            get { return _transform; }
        }

        public bool Initialized
        {
            get { return _initialized; }
        }

        public string Tag
        {
            get { return _tag; }
            set
            {
                if (_tag == value)
                    return;

                //TODO: Validate that this works
                string oldValue = _tag;
                _tag = value;
                if (TagChanged != null)
                    TagChanged(this, new ValueChangedEventArgs<string>(oldValue, _tag));
            }
        }

        public long ID
        {
            get { return _id; }
        }

        public bool Enabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled == value)
                    return;

                bool oldValue = _isEnabled;
                _isEnabled = value;
                if (EnabledChanged != null)
                    EnabledChanged(this, new ValueChangedEventArgs<bool>(oldValue, _isEnabled));
            }
        }

        /// <summary>
        ///     Get a copy of the list of all the components.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IComponent> Components
        {
            get { return _components.ToList(); }
        }

        #endregion

        //TODO: Add the Manager/ World / Scene in the constructor
        internal Entity(long id, string tag = "")
        {
            _transform = new TransformComponent(this);
            //AddComponent(_transform);
            _id = id;
            _tag = tag;
            Enabled = true;
        }

        internal void Initialize()
        {
            if (_initialized)
                return;

            _tempComponents.Clear();
            _tempComponents.AddRange(_components);

            _tempComponents.ForEach(c => c.Initialize());

            _initialized = true;
        }

        #region Components Handling

        public void AddComponent(IComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            if (!string.IsNullOrEmpty(component.Tag))
            {
                try
                {
                    _taggedComponents.Add(component.Tag, component);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(
                        string.Format("There is already a Component with the tag '{0}' in this entity.", component.Tag));
                }
            }

            _components.Add(component);

            var updateable = component as IUpdateableComponent;
            if (updateable != null)
            {
                _updateableComponents.Add(updateable);
                updateable.UpdateOrderChanged += OnComponentUpdateOrderChanged;
                OnComponentUpdateOrderChanged(this, EventArgs.Empty);
            }

            var drawable = component as IDrawableComponent;
            if (drawable != null)
            {
                _drawableComponents.Add(drawable);
                drawable.LayerChanged += OnComponentDrawOrderChanged;
                OnComponentDrawOrderChanged(this, EventArgs.Empty);
            }

            if (_initialized)
                component.Initialize();
        }

        public bool RemoveComponents(Type type)
        {
            return GetComponents(type).All(RemoveComponent);
        }

        public bool RemoveComponents<T>() where T : class, IComponent
        {
            return GetComponents<T>().All(RemoveComponent);
        }

        public bool RemoveComponent(string name)
        {
            if (_taggedComponents.ContainsKey(name))
                return RemoveComponent(_taggedComponents[name]) && _taggedComponents.Remove(name);

            return false;
        }

        public bool RemoveComponent(IComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            if (_components.Remove(component))
            {
                var updateable = component as IUpdateableComponent;
                if (updateable != null)
                {
                    _updateableComponents.Remove(updateable);
                    updateable.UpdateOrderChanged -= OnComponentUpdateOrderChanged;
                }

                var drawable = component as IDrawableComponent;
                if (drawable != null)
                {
                    _drawableComponents.Remove(drawable);
                    drawable.LayerChanged -= OnComponentDrawOrderChanged;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Returns the first component of the selected type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : class, IComponent
        {
            return GetComponents<T>().FirstOrDefault();
        }

        /// <summary>
        ///     Returns the first component of the selected type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IComponent GetComponent(Type type)
        {
            return GetComponents(type).FirstOrDefault();
        }

        /// <summary>
        ///     Returns all the components of the selected type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetComponents<T>() where T : class, IComponent
        {
            return _components.OfType<T>();
        }

        /// <summary>
        ///     Returns all the components of the selected type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IComponent> GetComponents(Type type)
        {
            return _components.Where(c => (c.GetType() == type)); 
        }

        /// <summary>
        ///     Returns a component with the tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IComponent GetComponent(string tag)
        {
            if (!_taggedComponents.ContainsKey(tag))
                return null;

            return _taggedComponents[tag] as BaseComponent;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            if (!Enabled)
                return;

            _tempUpdateableComponents.Clear();
            _tempUpdateableComponents.AddRange(_updateableComponents);

            foreach (IUpdateableComponent c in _tempUpdateableComponents.Where(c => c.Enabled))
            {
                if(!c.Started)
                    c.Start();

                c.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!Enabled) 
                return;

            _tempDrawableComponents.Clear();
            _tempDrawableComponents.AddRange(_drawableComponents);

            foreach (IDrawableComponent c in _tempDrawableComponents.Where(c => c.Visible))
                c.Draw(spriteBatch, gameTime);
        }

        #region Order Changing Event handling

        private void OnComponentUpdateOrderChanged(object sender, EventArgs e)
        {
            _updateableComponents.Sort(UpdateableSort);
        }

        private void OnComponentDrawOrderChanged(object sender, EventArgs e)
        {
            _drawableComponents.Sort(DrawableSort);
        }

        /// <summary>
        ///     Helper used in the sorting process
        /// </summary>
        /// <param name="entityA">first entity in question</param>
        /// <param name="entityB">second entity in question</param>
        /// <returns>the component's update order</returns>
        private static int UpdateableSort(IUpdateableComponent entityA, IUpdateableComponent entityB)
        {
            return entityA.UpdateOrder.CompareTo(entityB.UpdateOrder);
        }

        /// <summary>
        ///     Helper used in the sorting process
        /// </summary>
        /// <param name="entityA">first entity in question</param>
        /// <param name="entityB">second entity in question</param>
        /// <returns>the component's update order</returns>
        private static int DrawableSort(IDrawableComponent entityA, IDrawableComponent entityB)
        {
            return entityA.Layer.CompareTo(entityB.Layer);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Entity [{0}] : '{1}'", _id, Tag);
        }

        public object Clone()
        {
            //var e = new Entity();

            //TODO: Get a copy of each of the components (implement IClonable)
            //TODO: Set the GameObject to the new one.
            //TODO: Call AddComponent

            throw new NotImplementedException();
        }
    }
}