using System;
using System.Collections.Generic;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JPEngine.Components;
using Microsoft.Xna.Framework;

namespace JPEngine.Systems
{
    public class Box2DPhysicsSystem : System
    {
        private readonly World _world;
        //private IEnumerable<BodyComponent> _bodies; 

        public World World { get { return _world; } }

        public Box2DPhysicsSystem(Vector2 gravity)
        {
            _world = new World(gravity);
        }

        protected override void IntializeCore()
        {
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);
        }

        public override void Update(Dictionary<Type, IEnumerable<IComponent>> components, GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
        }
    }
}
