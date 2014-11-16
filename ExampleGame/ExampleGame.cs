using System.Collections.Specialized;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using JPEngine.Components;
using JPEngine.Components.Physics;
using JPEngine.Enums;
using JPEngine.Utils.ScriptConsole;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using JPEngine;
using JPEngine.Managers;
using JPEngine.Entities;
using ExampleGame.CustomComponents;

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
            Engine.Initialize(graphics, this);
            Engine.Console = new ScriptConsole(new ConsoleOptions(Content.Load<SpriteFont>("Fonts/ConsoleFont"))
            {
                Width = GraphicsDevice.Viewport.Width
            });

            Engine.Window.IsMouseVisible = true;

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
            //mainCamera.Transform.Scale *= 1.1f;
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
                e.AddComponent(new SpriteComponent(e, Engine.Textures["grass"])
                {
                    Layer = DrawingLayer.Background1
                });
                //e.Transform.Scale *= 1/4f;

                Engine.Entities.AddEntity(e);
            }
            
            Entity player = CreatePlayer(new Vector2(-350, 30));

            //CreateCrate(new Vector2(-200, 60), "platform", 64, 64, BodyType.Static);
            //CreateCrate(new Vector2(-136, 60), "platform", 64, 64, BodyType.Static);

            float cubeStartX = player.Transform.Position.X;
            float cubeStartY = player.Transform.Position.Y + 100;
            const int cubeWidth = 64;
            const int cubeHeight = 64;

            for (int x = 0; x < 10; x++)
            {
                int y = 0;
                //for (int y = 0; y < 3; y++)
                //{
                    int mod = x % 2;
                    string name = mod == 1 ? "ground" : "platform";
                    BodyType bodyType = mod == 1 ? BodyType.Dynamic : BodyType.Static;

                    CreateCrate(new Vector2(cubeStartX + (x * cubeWidth), cubeStartY + (y * cubeHeight)), name, 64, 64, bodyType);
                //}
            }
        }

        private Entity CreatePlayer(Vector2 pos)
        {
            Entity player = new Entity("player");

            player.Transform.Scale = new Vector2(0.5f, 0.5f);
            player.Transform.Position = pos;

            const int playerWidth = 64; //96;
            const int playerHeight = 64; //96;

            player.AddComponent(new SpriteComponent(player, Engine.Textures["crate"]));
            player.AddComponent(new PlayerInput(player));

            Body body = BodyFactory.CreateRectangle(
                Engine.Entities.PhysicsSystem.World,
                ConvertUnits.ToSimUnits(playerWidth*player.Transform.Scale.X),
                ConvertUnits.ToSimUnits(playerHeight*player.Transform.Scale.Y),
                1, player);

            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            body.Mass = 0;
            body.Friction = 0;
            body.LinearDamping = 1f;

            body.OnCollision += (a, b, contact) =>
            {
                Entity e1 = a.Body.UserData as Entity;
                Entity e2 = b.Body.UserData as Entity;
                if (e1 != null && e2 != null)
                {
                    if (e2.Tag == "ground")
                        b.Body.IgnoreGravity = false;
                }
                return true;
            };

            body.Position = new Vector2(
                ConvertUnits.ToSimUnits(player.Transform.Position.X),
                ConvertUnits.ToSimUnits(player.Transform.Position.Y));

            body.Rotation = player.Transform.Rotation;
            player.AddComponent(new BodyComponent(player, body));

            //e.AddComponent(new RectCollider(e) { Width = width, Height = height });
            //e.AddComponent(new RectRenderer(e, Rectangle.Empty, new Texture2D(Engine.Window.GraphicsDevice, 1, 1)));

            Engine.Entities.AddEntity(player);

            return player;
        }

        private static void CreateCrate(Vector2 pos, string name, int width = 64, int height = 64, BodyType bodyType = BodyType.Dynamic)
        {
            const int crate_width = 64;
            const int crate_height = 64;

            var e2 = new Entity(name);
            e2.Transform.Position.X = pos.X;
            e2.Transform.Position.Y = pos.Y;
            e2.Transform.Scale.X = width / crate_width;
            e2.Transform.Scale.Y = height / crate_height;

            Body body = BodyFactory.CreateRectangle(
                Engine.Entities.PhysicsSystem.World,
                ConvertUnits.ToSimUnits(width),
                ConvertUnits.ToSimUnits(height),
                100, 
                e2);

            body.Mass = 100;
            body.Friction = 100;
            body.LinearDamping = 3f;
            body.IgnoreGravity = true;
            body.FixedRotation = true;
            body.BodyType = bodyType;

            body.Position = new Vector2(
                ConvertUnits.ToSimUnits(e2.Transform.Position.X),
                ConvertUnits.ToSimUnits(e2.Transform.Position.Y));

            body.Rotation = e2.Transform.Rotation;

            e2.AddComponent(new BodyComponent(e2, body));
            e2.AddComponent(new SpriteComponent(e2, Engine.Textures["crate"]));

            Engine.Entities.AddEntity(e2);
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
