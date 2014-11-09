using JPEngine;
using JPEngine.ECS;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGame.CustomComponents
{
    public class PlayerInput : EntityComponent
    {
        
        public PlayerInput(Entity entity)
            :base(entity)
        {
            UpdateOrder = 0;
        }        

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float delta = gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            float speed = 100;

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["BtnQ"].Value))
            {
                Engine.SoundFX.Play("ammo_pickup", 0.05f);
                Console.WriteLine(string.Format("Val = {0}, Type = {1}", Engine.Settings["test"].Value, Engine.Settings["test"].ValueType));
                Console.WriteLine(string.Format("Val = {0}, Type = {1}", Engine.Settings["test2"].Value, Engine.Settings["test2"].ValueType));
                Console.WriteLine(string.Format("Val = {0}, Type = {1}", Engine.Settings["BtnQ"].Value, Engine.Settings["BtnQ"].ValueType));
            }


            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["UP"].Value))
            {
                GameObject.Transform.Position.Y -= speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["DOWN"].Value))
            {
                GameObject.Transform.Position.Y += speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["RIGHT"].Value))
            {
                GameObject.Transform.Position.X += speed * delta;
            }

            if (Engine.Input.IsKeyDown((Keys)Engine.Settings["LEFT"].Value))
            {
                GameObject.Transform.Position.X -= speed * delta;
            }

            base.Update(gameTime);
        }


    }
}
