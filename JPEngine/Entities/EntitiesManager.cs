﻿using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Components;
using JPEngine.Events;
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Entities
{
    public class EntitiesManager : Manager
    {
        /* TODO: Scenes
         * 
         * Split the entities by scenes => contains a list of entities
         * 
         * Put the Scene in the Entity construtor directly? Since we won't be using only one huge list in THIS class
         *  OR EntitiesManager.GetEntity(name) => _scenes.GetEntity(name) ..... Too slow?
         *  
         * Do we want to keep Entities in the Scene object OR keep a dictionnary here with the Scene as a key?
         * 
         * If we keep a dictonnary HERE, we would need to change the way Entities work to keep the components here too, 
         * so that they share the same Architecture mindset.         * 
         */

        private readonly List<Entity> _tempEntities = new List<Entity>(); //The list used to work with
        private readonly List<Entity> _entities = new List<Entity>();   //The core list of entities
        private readonly Dictionary<string, List<Entity>> _taggedEntities = new Dictionary<string, List<Entity>>();


        public event EventHandler<ListItemEventArgs<Entity>> EntityAdded;
        //TODO: Implement a Remove? Maybe only usefull when Scenes will be implented to move an Entity to another scene?
        //public event EventHandler<ListItemEventArgs<Entity>> EntityRemoved;

        internal EntitiesManager()
        {
        }

        protected override bool InitializeCore()
        {
            _tempEntities.Clear();
            _tempEntities.AddRange(_entities);

            _tempEntities.ForEach(e => e.Initialize());

            return true;
        }

        internal override void Update(GameTime gameTime)
        {
            _tempEntities.Clear();
            _tempEntities.AddRange(_entities);

            _tempEntities.ForEach(e => e.Update(gameTime));
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tempEntities.Clear();
            _tempEntities.AddRange(_entities);

            //TODO: Call SpriteManager which will be handling the layers? Or do it here?
            _tempEntities.ForEach(e => e.Draw(spriteBatch, gameTime));
        }

        public void AddEntity(Entity entity)
        {
            if(!entity.Initialized)
                entity.Initialize();

            _entities.Add(entity);
            AddTaggedEntity(entity);

            if(EntityAdded != null)
                EntityAdded(this, new ListItemEventArgs<Entity>(entity, ListItemAction.Added));
        }

        private void AddTaggedEntity(Entity entity)
        {
            if (!string.IsNullOrEmpty(entity.Tag))
            {
                entity.TagChanged += OnEntityTagChanged;

                //If the list does not already exist, create it
                if (!_taggedEntities.ContainsKey(entity.Tag))
                    _taggedEntities.Add(entity.Tag, new List<Entity>());

                _taggedEntities[entity.Tag].Add(entity);
            }
        }

        public Entity GetEntityByTag(string tag)
        {
            return GetEntitiesByTag(tag).FirstOrDefault();
        }

        public List<Entity> GetEntitiesByTag(string tag)
        {
            if (!_taggedEntities.ContainsKey(tag))
                return null;

            return _taggedEntities[tag];
        }
        
        private IEnumerable<T> GetAllComponentsOfType<T>() where T : class, IComponent
        {
            _tempEntities.Clear();
            _tempEntities.AddRange(_entities);

            return _tempEntities.SelectMany(e => e.GetComponents<T>());
        }

        private IEnumerable<IComponent> GetAllComponentsOfType(Type type)
        {
            _tempEntities.Clear();
            _tempEntities.AddRange(_entities);

            return _tempEntities.SelectMany(e => e.GetComponents(type));
        }

        public void ClearEntities()
        {
            //TODO: Use the RemoveEntity instead? So it will call the EntityRemoved event?
            _entities.Clear();
            _tempEntities.Clear();
            _taggedEntities.Clear();
        }

        private void OnEntityTagChanged(object sender, ValueChangedEventArgs<string> e)
        {
            var entity = sender as Entity;

            if (_taggedEntities.ContainsKey(e.OldValue))
            {
                //If the entity was already tagged, unsubscribe
                if (_taggedEntities[e.OldValue].Remove(entity))
                    entity.TagChanged -= OnEntityTagChanged;
            }

            AddTaggedEntity(entity);
        }

    }
}