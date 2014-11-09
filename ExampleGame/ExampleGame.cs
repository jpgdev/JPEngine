using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using JPEngine;
using JPEngine.Managers;
using JPEngine.ECS;
using ExampleGame.CustomComponents;
using JPEngine.ECS.Components;

namespace ExampleGame
{
    public class ExampleGame : Game
    {
        GraphicsDeviceManager graphics;  

        public ExampleGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            Engine.Initialize(graphics);

            Entity e = new Entity("player");
            DrawableSpriteComponent testDrawableComponent = new DrawableSpriteComponent(e)
                {
                    TextureName = "crate",
                    Width = 64,
                    Height = 64     
                };

            testDrawableComponent.Origin =  new Vector2(testDrawableComponent.Width/2, testDrawableComponent.Height/2);            
            e.AddComponent(testDrawableComponent);
            e.AddComponent(new PlayerInput(e));
            Engine.Entities.AddEntity(e);
            
            //Engine.WindowManager.SetScreenDimensions(1280, 720, false);

            base.Initialize();
        }

        protected override void LoadContent()
        {           
            Engine.LoadContent(Content);

            Engine.Textures.Add("crate", "Sprites/crate", true);
            Engine.Textures.Add("grass", "Tiles/grass", true);

            Engine.SoundFX.Add("ammo_pickup", "Sounds/ammo_pickup", true);            
            
            Engine.Settings.Add(new Setting<string>("test", "value!!!"));
            Engine.Settings.Add(new Setting<double>("test2", 0.5));
            Engine.Settings.Add(new Setting<Keys>("BtnQ", Keys.Q));
            Engine.Settings.Add(new Setting<Keys>("BtnF", Keys.F));

            Engine.Settings.Add(new Setting<Keys>("UP", Keys.Up));
            Engine.Settings.Add(new Setting<Keys>("LEFT", Keys.Left));
            Engine.Settings.Add(new Setting<Keys>("RIGHT", Keys.Right));
            Engine.Settings.Add(new Setting<Keys>("DOWN", Keys.Down));

            Engine.Settings.Add(new Setting<Keys>("PageUp", Keys.PageUp));
            Engine.Settings.Add(new Setting<Keys>("PageDown", Keys.PageDown));            
        }

        protected override void UnloadContent()
        {
            Engine.UnloadContent();            
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            
            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["BtnF"].Value))
            {
                //Engine.Settings.SaveSettings();
                //Engine.Settings.LoadSettings();
                Engine.WindowManager.IsFullScreen = !Engine.WindowManager.IsFullScreen;
            }

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageUp"].Value))
            {
                Engine.WindowManager.SetScreenDimensions(Engine.WindowManager.ScreenWidth + 32, Engine.WindowManager.ScreenHeight + 24);
            }

            if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["PageDown"].Value))
            {
                Engine.WindowManager.SetScreenDimensions(Engine.WindowManager.ScreenWidth - 32, Engine.WindowManager.ScreenHeight - 24);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Engine.Draw(gameTime);

            ////todo: Put in the SpriteManager
            //SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            //spriteBatch.Begin();
            //spriteBatch.Draw(Engine.Textures["crate"], new Vector2(10, 10), Color.White);
            //spriteBatch.Draw(Engine.Textures["crate"], new Vector2(100, 110), Color.White);
            //spriteBatch.Draw(Engine.Textures["crate"], new Vector2(200, 210), Color.White);


            //spriteBatch.End();
            //////

            base.Draw(gameTime);
        }
    }
}
