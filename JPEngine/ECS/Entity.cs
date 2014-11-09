using JPEngine.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPEngine.Events;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.ECS
{
    //Inspired from : https://xnaentitycomponents.codeplex.com/

    public class Entity
    {        
        private bool _isInitialized = false;
        private TransformComponent _transform;
        private string _tag = string.Empty;
        
        private List<IEntityComponent> _components = new List<IEntityComponent>();
        private List<IEntityUpdateable> _updateableComponents = new List<IEntityUpdateable>();
        private List<IEntityDrawable> _drawableComponents = new List<IEntityDrawable>();

        //The components on which the Drawing and Updating is performed on
        private List<IEntityComponent> _tempComponents = new List<IEntityComponent>();
        private List<IEntityUpdateable> _tempUpdateableComponents = new List<IEntityUpdateable>();
        private List<IEntityDrawable> _tempDrawableComponents = new List<IEntityDrawable>();

        private Dictionary<string, IEntityComponent> _sortedByNameComponents = new Dictionary<string, IEntityComponent>();
        
        public event EventHandler<ValueChangedEventArgs<string>> TagChanged;

        public bool IsInitialized { get { return _isInitialized; } }

        public string Tag
        {
            get { return _tag; }
            set
            {
                if(_tag != value)
                {
                    //TODO: Validate that this works
                    string oldValue = _tag;
                    _tag = value;
                    if (TagChanged != null)
                        TagChanged(this, new ValueChangedEventArgs<string>(oldValue, _tag));
                }
            }
        }       

        public TransformComponent Transform { get { return _transform; } }
        
        public Entity(string tag = "", bool autoAdd = false)
        {
            _transform = new TransformComponent(this);
            AddComponent(_transform);

            _tag = tag;

            if (autoAdd) 
                Engine.Entities.AddEntity(this);
        }

        public void Initialize()
        {
            if (_isInitialized)  return;
            
            _tempComponents.Clear();
            _tempComponents.AddRange(_components);

            _tempComponents.ForEach(c => c.Initialize());
            //TODO: Check if they are enabled before starting?
            _tempComponents.ForEach(c => c.Start());

            _isInitialized = true;
        }

        public void Update(GameTime gameTime)
        {
            _tempUpdateableComponents.Clear();
            _tempUpdateableComponents.AddRange(_updateableComponents);

            foreach (IEntityUpdateable c in _tempUpdateableComponents.Where(c => c.Enabled)) 
                c.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tempDrawableComponents.Clear();
            _tempDrawableComponents.AddRange(_drawableComponents);

            foreach (IEntityDrawable c in _tempDrawableComponents.Where(c => c.Visible))
                c.Draw(spriteBatch, gameTime);
        }


#region Components Handling

        public void AddComponent(IEntityComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("The component cannot be null.");

            //Type componentType = entityComponent.GetType();
            //if (GetComponent<componentType>())

            //TODO: Test if there is already a component of the same Type ?????
            //if(GetComponent<T>() != null)
            //    throw new ArgumentException(string.Format("There is already a Component of this type in this entity.", component.Name));

            if (!string.IsNullOrEmpty(component.Name))
            {
                try {
                    _sortedByNameComponents.Add(component.Name, component);
                }
                catch (ArgumentException e) {
                    throw new ArgumentException(string.Format("There is already a Component with the name {0} in this entity.", component.Name));
                }
            }

            _components.Add(component);
            //_sortedByNameComponents.Add(component.Name, component);

            IEntityUpdateable updateable = component as IEntityUpdateable;
            if (updateable != null)
            {
                _updateableComponents.Add(updateable);
                updateable.UpdateOrderChanged += OnComponentUpdateOrderChanged;
                OnComponentUpdateOrderChanged(this, EventArgs.Empty);
            }

            IEntityDrawable drawable = component as IEntityDrawable;
            if (drawable != null)
            {
                _drawableComponents.Add(drawable);
                drawable.DrawOrderChanged += OnComponentDrawOrderChanged;
                OnComponentDrawOrderChanged(this, EventArgs.Empty);
            }

            if (_isInitialized)
            {
                component.Initialize();
                component.Start();
            }
        }

        public bool RemoveComponents<T>() where T : class, IEntityComponent
        {
            return GetComponents<T>().All(c => RemoveComponent(c));
        }

        public bool RemoveComponent(string name)
        {
            if (_sortedByNameComponents.ContainsKey(name))
                return RemoveComponent(_sortedByNameComponents[name]);

            return false;
        }

        public bool RemoveComponent(IEntityComponent component)
        {
            if (component == null)
                  throw new ArgumentNullException("The component cannot be null.");

            if (_components.Remove(component))
            {
                IEntityUpdateable updateable = component as IEntityUpdateable;
                if (updateable != null)
                {
                    _updateableComponents.Remove(updateable);
                    updateable.UpdateOrderChanged -= OnComponentUpdateOrderChanged;
                }

                IEntityDrawable drawable = component as IEntityDrawable;
                if (drawable != null)
                {
                    _drawableComponents.Remove(drawable);
                    drawable.DrawOrderChanged -= OnComponentDrawOrderChanged;
                }
                return true;
            }
            return false;
        }

        public T GetComponent<T>() where T : class, IEntityComponent
        {
            return GetComponents<T>().FirstOrDefault();
        }

        public IEnumerable<T>GetComponents<T>() where T : class, IEntityComponent
        {
            return _sortedByNameComponents.Values.OfType<T>();
        }

        public IEntityComponent GetComponent(string name)
        {
            if (!_sortedByNameComponents.ContainsKey(name))
                return null;//throw new ArgumentOutOfRangeException(name);

            return _sortedByNameComponents[name] as EntityComponent;
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
        /// Helper used in the sorting process
        /// </summary>
        /// <param name="entityA">first entity in question</param>
        /// <param name="entityB">second entity in question</param>
        /// <returns>the component's update order</returns>
        private static int UpdateableSort(IEntityUpdateable entityA, IEntityUpdateable entityB)
        {
            return entityA.UpdateOrder.CompareTo(entityB.UpdateOrder);
        }

        /// <summary>
        /// Helper used in the sorting process
        /// </summary>
        /// <param name="entityA">first entity in question</param>
        /// <param name="entityB">second entity in question</param>
        /// <returns>the component's update order</returns>
        private static int DrawableSort(IEntityDrawable entityA, IEntityDrawable entityB)
        {
            return entityA.DrawOrder.CompareTo(entityB.DrawOrder);
        }

#endregion

    }
}
