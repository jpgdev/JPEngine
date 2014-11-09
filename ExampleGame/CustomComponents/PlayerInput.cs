using System;
using JPEngine;
using JPEngine.ECS;
using JPEngine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExampleGame.CustomComponents
{
    public class PlayerInput : EntityComponent
    {

        private DrawableSpriteComponent _drawableSpriteComponent;

        public PlayerInput(Entity entity)
            : base(entity)
        {
            UpdateOrder = 0;
        }

        public override void Start()
        {
            _drawableSpriteComponent = GameObject.GetComponent<DrawableSpriteComponent>();

            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            float delta = gameTime.ElapsedGameTime.Milliseconds/1000.0f;
            const float speed = 100;

            if (Engine.Input.IsKeyClicked((Keys) Engine.Settings["BtnQ"].Value))
            {
                Engine.SoundFX.Play("ammo_pickup", 0.05f);
                Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["test"].Value,
                    Engine.Settings["test"].ValueType);
                Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["test2"].Value,
                    Engine.Settings["test2"].ValueType);
                Console.WriteLine("Val = {0}, Type = {1}", Engine.Settings["BtnQ"].Value,
                    Engine.Settings["BtnQ"].ValueType);
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["UP"].Value))
            {
                GameObject.Transform.Position.Y -= speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["DOWN"].Value))
            {
                GameObject.Transform.Position.Y += speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["RIGHT"].Value))
            {
                GameObject.Transform.Position.X += speed*delta;
            }

            if (Engine.Input.IsKeyDown((Keys) Engine.Settings["LEFT"].Value))
            {
                GameObject.Transform.Position.X -= speed*delta;
            }

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageUp"].Value))
            {
                _drawableSpriteComponent.DrawOrder += 1;
                Console.WriteLine("Draw order = {0}", _drawableSpriteComponent.DrawOrder);
            }

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageDown"].Value))
            {
                _drawableSpriteComponent.DrawOrder -= 1;
                Console.WriteLine("Draw order = {0}", _drawableSpriteComponent.DrawOrder);
            }

            base.Update(gameTime);
        }
    }
}