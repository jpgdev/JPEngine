using JPEngine;
using JPEngine.Components;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame.CustomComponents
{
    public class CameraInput : BaseComponent
    {
        private float _speed = 100;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public CameraInput(Entity entity)
            : base(entity)
        {
        }

        public override void Update(GameTime gameTime)
        {
            float delta = gameTime.ElapsedGameTime.Milliseconds/1000.0f;

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["W"].Value))
            {
                Transform.Position.Y -= _speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["S"].Value))
            {
                Transform.Position.Y += _speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["D"].Value))
            {
                Transform.Position.X += _speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["A"].Value))
            {
                Transform.Position.X -= _speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["R"].Value))
            {
                Transform.Rotation = 0;
            }

            //if (Engine.Input.IsKeyDown((Keys)Engine.Settings["Q"].Value))
            //{
            //    Transform.Rotation += MathHelper.ToRadians(10f);
            //}

            //if (Engine.Input.IsKeyDown((Keys)Engine.Settings["E"].Value))
            //{
            //    Transform.Rotation -= MathHelper.ToRadians(10f);
            //}

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageUp"].Value))
            {
                Transform.Scale *= 1.1f;
            }

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageDown"].Value))
            {
                Transform.Scale *= 0.9f;
            }



            base.Update(gameTime);
        }
    }
}