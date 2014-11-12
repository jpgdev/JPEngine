using JPEngine;
using JPEngine.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame.CustomComponents
{
    public class PlayerInput : EntityComponent
    {
        public PlayerInput(Entity entity)
            : base(entity)
        {
            UpdateOrder = 0;
        }

        public override void Update(GameTime gameTime)
        {
            float delta = gameTime.ElapsedGameTime.Milliseconds/1000.0f;
            const float speed = 100;

            //if (Engine.Input.IsKeyClicked((Keys) Engine.Settings["BtnQ"].Value))
            //{
            //    Engine.SoundFX.Play("ammo_pickup", 0.05f);
            //    Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["test"].Value,
            //        Engine.Settings["test"].ValueType);
            //    Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["test2"].Value,
            //        Engine.Settings["test2"].ValueType);
            //    Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["BtnQ"].Value,
            //        Engine.Settings["BtnQ"].ValueType);
            //}

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["SpaceBar"].Value))
            {
                Transform.Position.Z += 1;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["C"].Value))
            {
                Transform.Position.Z -= 1;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["Q"].Value))
            {
                Transform.Rotation -= MathHelper.ToRadians(10f);
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["E"].Value))
            {
                Transform.Rotation += MathHelper.ToRadians(10f);
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["UP"].Value))
            {
                Transform.Position.Y -= speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["DOWN"].Value))
            {
                Transform.Position.Y += speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["RIGHT"].Value))
            {
                Transform.Position.X += speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["LEFT"].Value))
            {
                Transform.Position.X -= speed*delta;
            }


            base.Update(gameTime);
        }
    }
}