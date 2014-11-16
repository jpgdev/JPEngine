using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JPEngine;
using JPEngine.Components;
using JPEngine.Components.Physics;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame.CustomComponents
{
    public class PlayerInput : BaseComponent
    {
        private BodyComponent _bodyComponent;
        private Vector2 _maxVelocity = new Vector2(10, 20);

        public PlayerInput(Entity entity)
            : base(entity)
        {
            UpdateOrder = 0;
        }

        public override void Start()
        {
            _bodyComponent = GameObject.GetComponent<BodyComponent>();
            //TODO: If null => GameObject.OnComponentAdded += .... set the _bodyComponent

        }

        public override void Update(GameTime gameTime)
        {
            float deltaSinceLastUpdate = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            const float speed = 100;

            Vector2 moveVelocity = new Vector2();

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["Q"].Value))
            {
                Engine.SoundFX["ammo_pickup"].Play(0.05f, 0, 0);
            }

            //if (Engine.Input.IsKeyDown((Keys) Engine.Settings["SpaceBar"].Value))
            //{
            //    Transform.Position.Z += 1;
            //}

            //if (Engine.Input.IsKeyDown((Keys) Engine.Settings["C"].Value))
            //{
            //    Transform.Position.Z -= 1;
            //}

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["Q"].Value))
            {
                Rotate(-MathHelper.ToRadians(10f));
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["E"].Value))
            {
                Rotate(MathHelper.ToRadians(10f));
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["UP"].Value))
            {
                moveVelocity.Y -= 1;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["DOWN"].Value))
            {
                moveVelocity.Y += 1;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["RIGHT"].Value))
            {
                moveVelocity.X += 1;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["LEFT"].Value))
            {
                moveVelocity.X -= 1;
            }

            //moveVelocity.Normalize();

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["SpaceBar"].Value))
            {
                const float verticalJumpVelocity = 400;
                const float horizontalJumpVelocity = 50;

                //Todo: Checker si y touche le sol
                Jump(new Vector2(moveVelocity.X * horizontalJumpVelocity, -verticalJumpVelocity));
            }

            moveVelocity.X *= speed * deltaSinceLastUpdate;
            moveVelocity.Y *= speed * deltaSinceLastUpdate;

            Move(moveVelocity);
        }

        private void Rotate(float radians)
        {
            if (_bodyComponent != null)
                _bodyComponent.Body.Rotation += radians;
            else
                Transform.Rotation += radians;
        }

        private void Jump(Vector2 jumpVelocity)
        {
            if (_bodyComponent != null)
            {
                //jumpVelocity *= 10;
                Vector2 after = ConvertUnits.ToSimUnits(jumpVelocity);
                //_bodyComponent.Body.ApplyLinearImpulse(after);
                _bodyComponent.Body.ApplyForce(jumpVelocity);
                Console.WriteLine("Body : {0} ; {1}", jumpVelocity, after);
            }
            else
                Transform.Position += jumpVelocity;
        }

        private void Move(Vector2 direction)
        {
            if (_bodyComponent != null)
            {
                direction *= 10;

                direction.X = MathHelper.Clamp(direction.X, -_maxVelocity.X, _maxVelocity.X);
                direction.Y = MathHelper.Clamp(direction.Y, -_maxVelocity.Y, _maxVelocity.Y);

                _bodyComponent.Body.ApplyLinearImpulse(
                   new Vector2(
                       ConvertUnits.ToSimUnits(direction.X),
                       ConvertUnits.ToSimUnits(direction.Y)));

                //_bodyComponent.Body.LinearVelocity =
                //    new Vector2(
                //        ConvertUnits.ToSimUnits(direction.X),
                //        ConvertUnits.ToSimUnits(direction.Y));
            }
            else
                Transform.Position += direction;
        }
    }
}