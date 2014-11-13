using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using JPEngine;
using JPEngine.Managers;
using JPEngine.ECS;
using ExampleGame.CustomComponents;
using JPEngine.ECS.Components;

namespace ExampleGame
{
    public class ExampleGame : Game
    {
        readonly GraphicsDeviceManager graphics;  

        public ExampleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            Engine.Initialize(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {           
            Engine.Textures.Add("crate", "Sprites/crate", true);
            Engine.Textures.Add("grass", "Tiles/grass", true);
            Engine.Textures.Add("background", "Tiles/background", true);

            Engine.SoundFX.Add("ammo_pickup", "Sounds/ammo_pickup", true);
            
            Engine.Settings.Add(new Setting<Keys>("UP", Keys.Up));
            Engine.Settings.Add(new Setting<Keys>("LEFT", Keys.Left));
            Engine.Settings.Add(new Setting<Keys>("RIGHT", Keys.Right));
            Engine.Settings.Add(new Setting<Keys>("DOWN", Keys.Down));

            Engine.Settings.Add(new Setting<Keys>("W", Keys.W));
            Engine.Settings.Add(new Setting<Keys>("A", Keys.A));
            Engine.Settings.Add(new Setting<Keys>("D", Keys.D));
            Engine.Settings.Add(new Setting<Keys>("S", Keys.S));

            Engine.Settings.Add(new Setting<Keys>("Q", Keys.Q));
            Engine.Settings.Add(new Setting<Keys>("E", Keys.E));

            Engine.Settings.Add(new Setting<Keys>("SpaceBar", Keys.Space));
            Engine.Settings.Add(new Setting<Keys>("C", Keys.C));

            Engine.Settings.Add(new Setting<Keys>("R", Keys.R));

            Engine.Settings.Add(new Setting<Keys>("PageUp", Keys.PageUp));
            Engine.Settings.Add(new Setting<Keys>("PageDown", Keys.PageDown));
            
            

            LoadTestsEntities();
        }

        private void LoadTestsEntities()
        {
            //Setup & Add the base Camera entity
            Entity mainCamera = new Entity("_MainCamera", true);

            mainCamera.AddComponent(new CameraComponent(mainCamera));
            mainCamera.AddComponent(new CameraInput(mainCamera));
            Engine.Cameras.SetCurrent(mainCamera.GetComponent<CameraComponent>());


            //{
            //    Texture2D texture = Engine.Textures["grass"];
            //    var e = new Entity("ground");
            //    e.AddComponent(new DrawableSpriteComponent(e, texture)
            //    {
            //        Layer = DrawingLayer.Background1
            //    });

            //    Engine.Entities.AddEntity(e);
            //}

            {
                var e = new Entity("background");
                e.AddComponent(new DrawableSpriteComponent(e, Engine.Textures["background"])
                {
                    Layer = DrawingLayer.Background1
                });
                e.Transform.Scale *= 1/4f;

                Engine.Entities.AddEntity(e);
            }

            {
                var e = new Entity("player");
                //e.Transform.Scale = new Vector2(0.5f, 0.5f);
                
                e.AddComponent(new DrawableSpriteComponent(e, Engine.Textures["crate"]));
                e.AddComponent(new PlayerInput(e));
                e.AddComponent(new RectCollider(e) { Width = 96, Height = 96 });
                e.AddComponent(new RectRenderer(e, Rectangle.Empty, Engine.Window.CreateTexture(1, 1)));

                Engine.Entities.AddEntity(e);
            }


            {
                var e2 = new Entity();
                e2.Transform.Position.X = 100;
                e2.Transform.Position.Y = 100;

                e2.AddComponent(new DrawableSpriteComponent(e2, Engine.Textures["crate"]));

                Engine.Entities.AddEntity(e2);
            }

        }

        protected override void UnloadContent()
        {
            Engine.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            
            //if (Engine.Input.IsKeyClicked((Keys)Engine.Settings["BtnF"].Value))
            //{
            //    Engine.Settings.SaveSettings();
            //    Engine.Settings.LoadSettings();
            //    Engine.WindowManager.IsFullScreen = !Engine.WindowManager.IsFullScreen;
            //}

            //if (Engine.Input.IsKeyClicked(Keys.P))
            //{
            //    Engine.Window.ScreenWidth += 32;
            //    Engine.Window.ScreenHeight += 24;
            //    Engine.Window.ApplySettings();
            //}

            //if (Engine.Input.IsKeyClicked(Keys.M))
            //{
            //    Engine.Window.ScreenWidth -= 32;
            //    Engine.Window.ScreenHeight -= 24;
            //    Engine.Window.ApplySettings();
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
