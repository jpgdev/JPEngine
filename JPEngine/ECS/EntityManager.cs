﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using JPEngine.Events;

namespace JPEngine.ECS
{

    public class EntityManager : Manager
    {
        private List<Entity> _masterList = new List<Entity>();
        private List<Entity> _currentList = new List<Entity>();
        private Dictionary<string, List<Entity>> _taggedEntities = new Dictionary<string, List<Entity>>();
        
        internal EntityManager()
        {
        }
        
        protected override bool InitializeCore()
        {
            _currentList.Clear();
            _currentList.AddRange(_masterList);

            _currentList.ForEach(e => e.Initialize());

            return true;
        }

        internal override void Update(GameTime gameTime)
        {
            _currentList.Clear();
            _currentList.AddRange(_masterList);

            _currentList.ForEach(e => e.Update(gameTime));
        }

        internal void Draw()
        {
            _currentList.Clear();
            _currentList.AddRange(_masterList);

            //TODO: Call SpriteManager which will be handling the layers? Or do it here?
            _currentList.ForEach(e => e.Draw());
        }
        
        public void AddEntity(Entity entity)
        {
            _masterList.Add(entity);

            AddTaggedEntity(entity);
        }

        private void AddTaggedEntity(Entity entity)
        {
            if (!string.IsNullOrEmpty(entity.Tag))
            {
                entity.TagChanged += OnEntityTagChanged;
                           
                //If the list does not already exist, create it
                if(!_taggedEntities.ContainsKey(entity.Tag))
                    _taggedEntities.Add(entity.Tag, new List<Entity>());

                _taggedEntities[entity.Tag].Add(entity);  
            }
        }

        public List<Entity> GetEntitiesByTag(string tag)
        {
            if (!_taggedEntities.ContainsKey(tag))
                return null;

            return _taggedEntities[tag];
        }

        public void ClearEntities()
        {
            _masterList.Clear();
            _currentList.Clear();
            _taggedEntities.Clear();
        }
        
        private void OnEntityTagChanged(object sender, ValueChangedEventArgs<string> e)
        {
            Entity entity = sender as Entity;

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
