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
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
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
            Engine.Textures.Add("background", "Background/clouds_and_trees", true);
            

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
            mainCamera.AddComponent(new AutoScrollingCamera(mainCamera) {Speed = 20, IsHorizontal = false});
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
                e.AddComponent(new ParallaxScrollingComponent(e, Engine.Textures["background"])
                {
                    Layer = DrawingLayer.Background3,
                    ParallaxRatio = 0.8f
                });
                //e.Transform.Scale *= 1/4f;

                Engine.Entities.AddEntity(e);
            }


            //{
            //    var e = new Entity("crate_cloud");
            //    e.AddComponent(new ParallaxScrollingComponent(e, Engine.Textures["crate"])
            //    {
            //        Layer = DrawingLayer.Background2,
            //        ParallaxRatio = 0.5f
            //    });
            //    e.Transform.Position = new Vector2(100, -100);
            //    //e.Transform.Scale *= 1/4f;

            //    Engine.Entities.AddEntity(e);
            //}

            {
                var e = new Entity("crate_cloud2");
                e.AddComponent(new ParallaxScrollingComponent(e, Engine.Textures["crate"])
                {
                    Layer = DrawingLayer.Background2,
                    ParallaxRatio = -1.2f
                });
                e.Transform.Position = new Vector2(-170, -180);
                //e.Transform.Scale *= 1/4f;

                Engine.Entities.AddEntity(e);
            }


            
            Entity player = CreatePlayer(new Vector2(-350, 30));

            CreateCrate(new Vector2(-200, 60), "platform", 64, 64, BodyType.Static, Color.LightBlue, 0);
            //CreateCrate(new Vector2(-136, 60), "platform", 64, 64, BodyType.Static);

            float cubeStartX = player.Transform.Position.X;
            float cubeStartY = player.Transform.Position.Y + 100;
            const int cubeWidth = 64;
            const int cubeHeight = 64;


            Vector2 lastPos = new Vector2(cubeStartX + 5, cubeStartY + 5);
            for (int x = 0; x < 10; x++)
            {
                int y = 0;
                int mod = x % 2;
                string name = mod == 1 ? "ground" : "platform";
                BodyType bodyType = mod == 1 ? BodyType.Dynamic : BodyType.Static;
                
                CreateCrate(lastPos , name, 64, 64, bodyType);

                lastPos.X += cubeWidth + 5;
                //lastPos.Y += cubeHeight + 5;
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
                1);

            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            body.Mass = 0;
            body.Friction = 1f;
            body.LinearDamping = 1f;

            body.Position = new Vector2(
                ConvertUnits.ToSimUnits(player.Transform.Position.X),
                ConvertUnits.ToSimUnits(player.Transform.Position.Y));

            body.Rotation = player.Transform.Rotation;
            BodyComponent bodyComponent = new BodyComponent(player, body);

            bodyComponent.OnCollision += (sender, args) =>
            {
                if (args.BodyComponentB.GameObject.Tag == "ground")
                    args.BodyComponentB.Body.IgnoreGravity = false;
            };


            player.AddComponent(bodyComponent);

            //e.AddComponent(new RectCollider(e) { Width = width, Height = height });
            //e.AddComponent(new RectRenderer(e, Rectangle.Empty, new Texture2D(Engine.Window.GraphicsDevice, 1, 1)));

            Engine.Entities.AddEntity(player);

            return player;
        }

        private static void CreateCrate(Vector2 pos, string name, int width = 64, int height = 64, BodyType bodyType = BodyType.Dynamic, Color? color = null, float friction = 0.2f)
        {
            const int crateWidth = 64;
            const int crateHeight = 64;

            var e = new Entity(name);
            e.Transform.Position.X = pos.X;
            e.Transform.Position.Y = pos.Y;
            e.Transform.Scale.X = width / (float)crateWidth;
            e.Transform.Scale.Y = height / (float)crateHeight;

            Body body = BodyFactory.CreateRectangle(
                Engine.Entities.PhysicsSystem.World,
                ConvertUnits.ToSimUnits(width),
                ConvertUnits.ToSimUnits(height),
                100);

            body.Mass = 100;
            body.Friction = friction;
            body.LinearDamping = 3f;
            body.IgnoreGravity = true;
            body.FixedRotation = true;
            body.BodyType = bodyType;

            body.Position = new Vector2(
                ConvertUnits.ToSimUnits(e.Transform.Position.X),
                ConvertUnits.ToSimUnits(e.Transform.Position.Y));

            body.Rotation = e.Transform.Rotation;
            
            e.AddComponent(new BodyComponent(e, body));

            Color c = color ?? Color.White;

            e.AddComponent(new SpriteComponent(e, Engine.Textures["crate"]) { DrawingColor = c });

            Engine.Entities.AddEntity(e);
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
