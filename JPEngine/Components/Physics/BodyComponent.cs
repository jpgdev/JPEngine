using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using JPEngine.Entities;
using Microsoft.Xna.Framework;

namespace JPEngine.Components.Physics
{
    public class BodyComponent : BaseComponent
    {
        private Body _body;
        public Body Body
        {
            get { return _body; }
            protected set
            {   
                _body = value;
                _body.UserData = GameObject;
            }
        }

        public BodyComponent(Entity entity, Body body) 
            : base(entity)
        {
            _body = body;
            _body.UserData = entity;
            _body.OnCollision += OnCollision;
        }

        protected bool OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            //Entity e2 = fixtureB.Body.UserData as Entity;
            //if (e2 != null)
            //{
            //    DrawableSpriteComponent dc = e2.GetComponent<DrawableSpriteComponent>();
            //    if (dc != null)
            //        dc.DrawingColor = new Color(Color.LightBlue, 135);
            //}

            return true;
        }

        public override void Update(GameTime gameTime)
        {
            Transform.Position = ConvertUnits.ToDisplayUnits(Body.Position);
            Transform.Rotation = ConvertUnits.ToDisplayUnits(Body.Rotation);
        }
    }
}
