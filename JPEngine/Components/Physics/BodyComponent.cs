using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using JPEngine.Entities;
using Microsoft.Xna.Framework;

namespace JPEngine.Components.Physics
{

    //TODO: Use this correctly to wrap the Farseer OnCollision event
    //public delegate bool CollisionEventHandler(BodyComponent a, BodyComponent b, Contact c);

    public class BodyCollisionEventArgs : EventArgs
    {
        public BodyComponent BodyComponentA { get; private set; }
        public BodyComponent BodyComponentB { get; private set; }
        public Contact Contact { get; private set; }

        public bool IsColliding { get; set; }

        public BodyCollisionEventArgs(BodyComponent bodyComponentA, BodyComponent bodyComponentB, Contact contact)
        {
            BodyComponentA = bodyComponentA;
            BodyComponentB = bodyComponentB;
            Contact = contact;
            IsColliding = true;
        }
    }

    public class BodyComponent : BaseComponent
    {
        private Body _body;
        public Body Body
        {
            get { return _body; }
            protected set
            {   
                _body = value;
                _body.UserData = this;
            }
        }

        public event EventHandler<BodyCollisionEventArgs> OnCollision;
        public event EventHandler<BodyCollisionEventArgs> OnSeparation;
        //public event CollisionEventHandler OnCollision;

        public BodyComponent(Entity entity, Body body) 
            : base(entity)
        {
            _body = body;
            _body.UserData = this;
            _body.OnCollision += OnCollisionInternal;
            _body.OnSeparation += OnSeparationInternal;
        }

        protected bool OnCollisionInternal(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            BodyComponent compA = fixtureA.Body.UserData as BodyComponent;
            BodyComponent compB = fixtureB.Body.UserData as BodyComponent;

            if (compA == null || compB == null)
            {
                //TODO: Not supposed to happen
                //return or throw?
                return true;
            }

            //TODO: Make own wrapper event and get the value of each (bool) to pass them down to the Farseer Event
            //if (OnCollision != null)
            //    OnCollision(compA, compB, contact);


            //TODO: Has a HUGE flaw, if someone put IsColliding to false, then another component to true, the last one will always win
            // I need something like : isColliding &= col.IsColling; // for each collider events
            BodyCollisionEventArgs col = new BodyCollisionEventArgs(compA, compB, contact);

            if (OnCollision != null)
                OnCollision(this, col);

            return col.IsColliding;
        }

        protected void OnSeparationInternal(Fixture fixtureA, Fixture fixtureB)
        {
            BodyComponent compA = fixtureA.Body.UserData as BodyComponent;
            BodyComponent compB = fixtureB.Body.UserData as BodyComponent;

            if (compA == null || compB == null)
            {
                //TODO: Not supposed to happen
                //return or Throw?
                return;
            }

            if (OnSeparation != null)
                OnSeparation(this, new BodyCollisionEventArgs(compA, compB, null));
        }

        public override void Update(GameTime gameTime)
        {
            Transform.Position = ConvertUnits.ToDisplayUnits(Body.Position);
            Transform.Rotation = ConvertUnits.ToDisplayUnits(Body.Rotation);
        }
    }
}
