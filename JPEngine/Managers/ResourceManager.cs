using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JPEngine.Events;

namespace JPEngine.Managers
{
    public abstract class ResourceManager<T> : Manager where T : IDisposable
    {
#region Attributes

        protected Dictionary<string, string> _paths;
        protected Dictionary<string, T> _resources;
        private ContentManager _content;

#endregion


#region Events
                
        protected event EventHandler<ItemEventArgs<string>> ResourceAdded;
        protected event EventHandler<ItemEventArgs<string>> ResourceRemoved;

        protected event EventHandler<ItemEventArgs<T>> ResourceLoaded;
        protected event EventHandler<ItemEventArgs<T>> ResourceUnloaded;


        private void OnResourceAdded(ItemEventArgs<string> eventArgs)
        {
            if (ResourceAdded != null)
            {
                ResourceAdded(this, eventArgs);
            }
        }

        private void OnResourceRemoved(ItemEventArgs<string> eventArgs)
        {
            if (ResourceRemoved != null)
            {
                ResourceRemoved(this, eventArgs);
            }
        }

        private void OnResourceLoaded(ItemEventArgs<T> eventArgs)
        {
            if (ResourceLoaded != null)
            {
                ResourceLoaded(this, eventArgs);
            }
        }

        private void OnResourceUnloaded(ItemEventArgs<T> eventArgs)
        {
            if (ResourceUnloaded != null)
            {
                ResourceUnloaded(this, eventArgs);
            }
        }

#endregion

#region Properties
        protected ContentManager Content
        {
            get { return _content; }
        }

        /// <summary>
        /// Returns all the loaded texture names.
        /// </summary>
        public string[] Loaded
        {
            get { return _resources.Keys.ToArray(); }            
        }

        /// <summary>
        /// Returns all the added texture names.
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

        public T GetResource(string name)
        {
            return _resources[name];
        }

#endregion

#region Constructors
        
        internal ResourceManager(ContentManager content)
            :base()    
        {
            _content = content;
            _resources = new Dictionary<string, T>();
            _paths = new Dictionary<string, string>();
        }
       
        
#endregion


#region Methods

        public bool Add(string name, string path, bool forceLoad = false)
        {
            bool added = false;

            if(!_paths.ContainsKey(name))
            {
                _paths.Add(name, path);
                OnResourceAdded(new ItemEventArgs<string>(path, ItemAction.Added));
                added = true;

                if (forceLoad) 
                    Load(name); //added &= Load(name);
            }

            return added;
        }

        public bool Remove(string name)
        {
            bool removed = false;

            if (_paths.ContainsKey(name))
            {
                string p = _paths[name];
                if (_paths.Remove(name))
                {
                    OnResourceRemoved(new ItemEventArgs<string>(p, ItemAction.Removed));
                    removed = true;
                }
            }

            return removed;
        }         

        public bool Load(string name)
        {
            bool loaded = false;

            if (!_resources.ContainsKey(name) && _paths.ContainsKey(name))
            {                
                //Will cause an error if the file does not exist
                //TODO: Add a Try/Catch?
                _resources.Add(name, _content.Load<T>(_paths[name]));

                OnResourceLoaded(new ItemEventArgs<T>(_resources[name], ItemAction.Added));
            }
            
            return loaded;
        }

        public bool Load(params string[] names)
        {
            //bool allLoaded = true;
            //foreach(string n in names)
            //{
            //    allLoaded &= Load(n);
            //}
            //return allLoaded;
            return names.All(n => Load(n));
        }

        public bool Unload(string name)
        {
            bool removed = false;

            if (_resources.ContainsKey(name))
            {
                T r = _resources[name];
                if (_resources.Remove(name))
                {
                    OnResourceUnloaded(new ItemEventArgs<T>(r, ItemAction.Removed));
                    r.Dispose(); //TODO: May cause a problem because it may be Disposed when someone tries to access it in the EventHandler?
                    removed = true;
                }
            }

            return removed;
        }

        public bool Unload(params string[] names)
        {
            //bool allUnloaded = true;
            //foreach(string n in names)
            //{
            //    allUnloaded &= Unload(n);
            //}
            //return allUnloaded;
            return names.All(n => Unload(n));
        }
        
        public bool IsLoaded(string name)
        {
            return _resources.ContainsKey(name);
        }
      
        public bool IsAdded(string name)
        {
            return _paths.ContainsKey(name);
        }

#endregion
                
    }
}
