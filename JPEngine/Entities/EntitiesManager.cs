using System;
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
        private readonly List<Entity> _tempEntities = new List<Entity>(); //The list used to work with
        private readonly List<Entity> _entities = new List<Entity>();   //The core list of entities
        private readonly Dictionary<string, List<Entity>> _taggedEntities = new Dictionary<string, List<Entity>>();

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
            entity.Initialize();

            _entities.Add(entity);
            AddTaggedEntity(entity);
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