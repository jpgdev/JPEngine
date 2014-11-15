using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Events;
using Microsoft.Xna.Framework.Content;

namespace JPEngine.Managers
{
    //TODO: Launch exceptions instead of returning false? (for Add & Load)

    public abstract class ResourceManager<T> : Manager
    {
        #region Attributes

        private readonly ContentManager _content;
        protected Dictionary<string, string> _paths;
        protected Dictionary<string, T> _resources;

        #endregion

        #region Events

        protected event EventHandler<ListItemEventArgs<string>> ResourceAdded;
        protected event EventHandler<ListItemEventArgs<string>> ResourceRemoved;

        protected event EventHandler<ListItemEventArgs<T>> ResourceLoaded;
        protected event EventHandler<ListItemEventArgs<T>> ResourceUnloaded;


        private void OnResourceAdded(ListItemEventArgs<string> eventArgs)
        {
            if (ResourceAdded != null)
                ResourceAdded(this, eventArgs);
        }

        private void OnResourceRemoved(ListItemEventArgs<string> eventArgs)
        {
            if (ResourceRemoved != null)
                ResourceRemoved(this, eventArgs);
        }

        private void OnResourceLoaded(ListItemEventArgs<T> eventArgs)
        {
            if (ResourceLoaded != null)
                ResourceLoaded(this, eventArgs);
        }

        private void OnResourceUnloaded(ListItemEventArgs<T> eventArgs)
        {
            if (ResourceUnloaded != null)
                ResourceUnloaded(this, eventArgs);
        }

        #endregion

        #region Properties

        protected ContentManager Content
        {
            get { return _content; }
        }

        /// <summary>
        ///     Returns all the loaded texture names.
        /// </summary>
        public string[] Loaded
        {
            get { return _resources.Keys.ToArray(); }
        }

        /// <summary>
        ///     Returns all the added texture names.
        /// </summary>
        public string[] Added
        {
            get { return _paths.Keys.ToArray(); }
        }

        public int AmountLoaded
        {
            get { return _resources.Count; }
        }

        public int AmountAdded
        {
            get { return _paths.Count; }
        }

        public T this[string name]
        {
            get { return GetResource(name); }
        }

        public T GetResource(string name, bool forceLoad = false)
        {
            if (forceLoad && !_resources.ContainsKey(name))
                Load(name);

            return _resources[name];
        }

        #endregion

        #region Constructors

        internal ResourceManager(ContentManager content)
        {
            _content = content;
            _resources = new Dictionary<string, T>();
            _paths = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        public override void UnloadContent()
        {
            base.UnloadContent();

            var temp = new Dictionary<string, T>(_resources);
            temp.All(r => Unload(r.Key));
            _paths.Clear();
            _resources.Clear();
        }

        /// <summary>
        ///     Add a resource path to be loaded later.
        /// </summary>
        /// <param name="name">The name of the resource which will be the key used.</param>
        /// <param name="path">The path of the resource file.</param>
        /// <param name="forceLoad">Whether or not to load the resource immediatly.</param>
        /// <returns>
        ///     If the path was succesfully added. If <see cref="forceLoad" /> is set to true, it will also check if it was
        ///     loaded correctly.
        /// </returns>
        public bool Add(string name, string path, bool forceLoad = false)
        {
            bool added = false;

            if (!_paths.ContainsKey(name))
            {
                _paths.Add(name, path);
                OnResourceAdded(new ListItemEventArgs<string>(path, ListItemAction.Added));
                added = true;

                if (forceLoad)
                    added &= Load(name);
            }

            return added;
        }


        /// <summary>
        ///     Remove and Unload a resource.
        /// </summary>
        /// <param name="name">The name of the resource which is the key.</param>
        /// <returns>Whether or not the resource was correctly removed and unloaded.</returns>
        public bool Remove(string name)
        {
            bool removed = false;

            if (_paths.ContainsKey(name))
            {
                string p = _paths[name];
                if (_paths.Remove(name))
                {
                    removed = true;
                    OnResourceRemoved(new ListItemEventArgs<string>(p, ListItemAction.Removed));

                    if (IsResourceLoaded(name))
                        removed = Unload(name);
                }
            }

            return removed;
        }

        /// <summary>
        ///     Load a resource using the Path linked to the name.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <returns>Whether or not the resource was succesfully loaded.</returns>
        public bool Load(string name)
        {
            if (!_resources.ContainsKey(name))
            {
                if (!_paths.ContainsKey(name))
                    throw new ArgumentOutOfRangeException(string.Format("The resource path for '{0}' does not exist.",
                        name));

                _resources.Add(name, _content.Load<T>(_paths[name]));

                OnResourceLoaded(new ListItemEventArgs<T>(_resources[name], ListItemAction.Added));
                return true;
            }

            return false;
        }

        public bool Load(params string[] names)
        {
            return names.All(Load);
        }

        /// <summary>
        ///     Unload a resource and dispose of it.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <returns>Whether of not the resource was succesfully unloaded.</returns>
        public bool Unload(string name)
        {
            bool removed = false;

            if (_resources.ContainsKey(name))
            {
                T r = _resources[name];
                if (_resources.Remove(name))
                {
                    OnResourceUnloaded(new ListItemEventArgs<T>(r, ListItemAction.Removed));
                    if(r is IDisposable)
                        (r as IDisposable).Dispose();
                    //TODO: May cause a problem because it may be Disposed when someone tries to access it in the EventHandler?
                    removed = true;
                }
            }
            return removed;
        }

        public bool Unload(params string[] names)
        {
            return names.All(Unload);
        }

        /// <summary>
        ///     Check if the resource with the name is already loaded.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsResourceLoaded(string name)
        {
            return _resources.ContainsKey(name) && _resources[name] != null;
        }

        /// <summary>
        ///     Check if there is already a resource path with the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsResourcePathAdded(string name)
        {
            return _paths.ContainsKey(name);
        }

        #endregion
    }
}