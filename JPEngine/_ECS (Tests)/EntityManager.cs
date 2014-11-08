//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JPEngine.ECS
//{

//    //Source : http://cbpowell.wordpress.com/2012/12/05/entity-component-game-programming-using-jruby-and-libgdx-part-2/

//    internal class EntityManager 
//    {



//        /*
//         * Source : http://piemaster.net/2011/07/entity-component-artemis/
//         * Convenience
//         *      All Artemis processing occurs within a managed World object. For convenience, the world object contains four managers that make your job almost too easy:
//         *
//         *      EntityManager manages the creation and removal of entities and their components, and allows components to be retrieved from entities by type.
//         * 
//         *      GroupManager allows entities to be assigned to unique groups (string IDs, such as “ENEMIES”) and retrieved as a group at any time in any system.
//         *
//         *      TagManager allows individual entities to be assigned uniquely identifying tags (again, string IDs, such as “PLAYER”) and retrieved individually, similarly to groups.
//         * 
//         *      SystemManager stores all of your systems, allowing them to be retrieved individually by type if necessary (for instance, the RenderSystem may need to communicate with the CameraSystem).
//         * 
//         */





//        /*Ideas:
//         * 
//         * - This class needs a GetEntityByTag(tag); method
//         *                              &
//         *                      GetEntitiesByGroup(string groupName);
//         * 
//         * 
//         * 
//         * 
//         * 
//         * 
//         * 
//         */ 




//        public Guid CreateBasicEntity()
//        {
//            return Guid.NewGuid();
//        }




//        private Dictionary<Guid, string> _idToTags;
//        private Dictionary<string, List<Guid>> _tagsToIds;
//        private Dictionary<Type, Dictionary<Guid, List<Component>>> _componentsStore;


//        public EntityManager()
//        {
//            var id = Guid.NewGuid();
//            _idToTags = new Dictionary<Guid, string>();
//            _tagsToIds = new Dictionary<string, List<Guid>>();
//            _componentsStore = new Dictionary<Type, Dictionary<Guid, List<Component>>>();
//        }


//        public Dictionary<Guid, List<Component>> EntitiesWithComponentType(Type type)
//        {
//            return _componentsStore.ContainsKey(type) ? _componentsStore[type] : null;
//        }

//        public Guid CreateBasicEntity()
//        {
//            return Guid.NewGuid();
//        }

//        public Guid CreateTaggedEntity(string tag)
//        {
//            if (string.IsNullOrEmpty(tag))
//                throw new ArgumentNullException("tag");

//            Guid id = CreateBasicEntity();
//            _idToTags[id] = tag;
//            if (_tagsToIds.ContainsKey(tag))
//                _tagsToIds[tag].Add(id);
//            else
//                _tagsToIds.Add(tag, new List<Guid>() { id });

//            return id;
//        }

//        public void AddComponent(Guid entityGuid, Component component)
//        {
//            if (component == null)
//                throw new ArgumentNullException("component");

//            if (!_componentsStore.ContainsKey(component.GetType()))
//            {
//                _componentsStore.Add(component.GetType(), new Dictionary<Guid, List<Component>>());
//            }

//            var dict = _componentsStore[component.GetType()];
//            if (!dict.ContainsKey(entityGuid))
//            {
//                dict.Add(entityGuid, new List<Component>());
//            }

//            dict[entityGuid].Add(component);


//            ////Validate that the type is a component
//            //if (!component.IsAssignableFrom(typeof(Component).GetType()))
//            //{
//            //    throw new ArgumentException("")
//            //}
//        }

//        public bool HasComponentOfType(Guid entityID, Type componentType)
//        {
//            return GetComponentOfType(entityID, componentType) != null;
//        }

//        public Component GetComponentOfType(Guid entityID, Type componentType)
//        {
//            if (componentType == null)
//                throw new ArgumentNullException("componentType");


//            if (!_componentsStore.ContainsKey(componentType))
//                return null;

//            var store = _componentsStore[componentType];


//            //TODO: Pas trop sur de comment le getter, parce que c'est une list, donc y'en a plusieurs, mais est-ce que c'est logique?
//            return (store.ContainsKey(entityID) && store[entityID].Count > 0) ? store[entityID][0] : null;
//        }

//        public bool HasComponent(Guid entityID, Component component)
//        {
//            if (component == null)
//                throw new ArgumentNullException("component");

//            if (!_componentsStore.ContainsKey(component.GetType()))
//                return false;

//            var store = _componentsStore[component.GetType()];

//            return store.ContainsKey(entityID) && store[entityID].Contains(component);
//        }

//    }
//}
