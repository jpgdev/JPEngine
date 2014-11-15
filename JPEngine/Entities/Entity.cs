﻿using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Components;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Entities
{
    //Inspired from : https://xnaentitycomponents.codeplex.com/

    public class Entity : ICloneable
    {
        private readonly List<IComponent> _components = new List<IComponent>();
        private readonly List<IDrawableComponent> _drawableComponents = new List<IDrawableComponent>();
        private readonly List<IUpdateableComponent> _updateableComponents = new List<IUpdateableComponent>();

        private readonly Dictionary<string, IComponent> _taggedComponents = new Dictionary<string, IComponent>();

        //The components on which the Drawing and Updating is performed on
        //TODO: Is it better to clear the lists before they are used, or simply create new ones each time we use them (ex. _tempComponents = _components.ToList();)?
        private readonly List<IComponent> _tempComponents = new List<IComponent>();
        private readonly List<IDrawableComponent> _tempDrawableComponents = new List<IDrawableComponent>();
        private readonly List<IUpdateableComponent> _tempUpdateableComponents = new List<IUpdateableComponent>();

        private readonly TransformComponent _transform;

        private bool _initialized;
        private string _tag = string.Empty;

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

        /// <summary>
        ///     Get a copy of the list of all the components.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IComponent> Components
        {
            get { return _components.ToList(); }
        }

        #endregion

        public Entity(string tag = "", bool autoAdd = false)
        {
            _transform = new TransformComponent(this);
            //AddComponent(_transform);

            _tag = tag;
            
            if (autoAdd)
                Engine.Entities.AddEntity(this);
        }

        public object Clone()
        {
            var e = new Entity();

            //TODO: Get a copy of each of the components (implement IClonable)
            //TODO: Set the GameObject to the new one.
            //TODO: Call AddComponent

            throw new NotImplementedException();
        }

        public event EventHandler<ValueChangedEventArgs<string>> TagChanged;

        public void Initialize()
        {
            if (_initialized)
                return;

            _tempComponents.Clear();
            _tempComponents.AddRange(_components);

            _tempComponents.ForEach(c => c.Initialize());

            //TODO: Check if they are enabled before starting?
            _tempComponents.ForEach(c => c.Start());

            _initialized = true;
        }

        public void Update(GameTime gameTime)
        {
            _tempUpdateableComponents.Clear();
            _tempUpdateableComponents.AddRange(_updateableComponents);

            foreach (IUpdateableComponent c in _tempUpdateableComponents.Where(c => c.Enabled))
                c.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tempDrawableComponents.Clear();
            _tempDrawableComponents.AddRange(_drawableComponents);

            foreach (IDrawableComponent c in _tempDrawableComponents.Where(c => c.Visible))
                c.Draw(spriteBatch, gameTime);
        }

        #region Components Handling

        public void AddComponent(IComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("The component cannot be null.");

            //Type componentType = entityComponent.GetType();
            //if (GetComponent<componentType>())

            //TODO: Test if there is already a component of the same Type ?????
            //if(GetComponent<T>() != null)
            //    throw new ArgumentException(string.Format("There is already a Component of this type in this entity.", component.Name));

            if (!string.IsNullOrEmpty(component.Tag))
            {
                try
                {
                    _taggedComponents.Add(component.Tag, component);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException(
                        string.Format("There is already a Component with the name {0} in this entity.", component.Tag));
                }
            }

            _components.Add(component);
            //_sortedByNameComponents.Add(component.Name, component);

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
            {
                component.Initialize();
                component.Start();
            }
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
                throw new ArgumentNullException("The component cannot be null.");

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
                return null; //throw new ArgumentOutOfRangeException(name);

            return _taggedComponents[tag] as BaseComponent;
        }

        #endregion

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
    }
}