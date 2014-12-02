using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using JPEngine.Components;
using JPEngine.Components.Physics;
using JPEngine.Enums;
using JPEngine.Graphics;
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
        private readonly GraphicsDeviceManager graphics;
        private PrimitiveBatch _primitiveBatch;

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
            Engine.Window.Width = 1280;
            Engine.Window.Height = 720;
            Engine.Managers.Add(typeof(ScriptConsole), new ScriptConsole(new ConsoleOptions(Content.Load<SpriteFont>("Fonts/ConsoleFont"))
            {
                Width = GraphicsDevice.Viewport.Width
            }));

            Engine.Window.IsMouseVisible = true;

            _primitiveBatch = new PrimitiveBatch(Engine.Window.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Engine.Textures.Add("crate", "Sprites/crate", true);
            Engine.Textures.Add("grass", "Tiles/grass", true);
            Engine.Textures.Add("lumberjack", "Sprites/lumberjack", true);
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
            Entity mainCamera = Engine.Entities.CreateEntity("_MainCamera");
            //mainCamera.Transform.Scale *= 1.1f;
            mainCamera.AddComponent(new CameraComponent(mainCamera));
            mainCamera.AddComponent(new CameraInput(mainCamera));
            mainCamera.AddComponent(new AutoMovingComponent(mainCamera) {Speed = 20, IsHorizontal = true});
            Engine.Cameras.SetCurrent(mainCamera.GetComponent<CameraComponent>());
          
            {
                var e = Engine.Entities.CreateEntity("background");
                e.AddComponent(new ParallaxScrollingComponent(e, Engine.Textures["background"])
                {
                    Layer = DrawingLayer.Background3,
                    ParallaxRatio = 1f
                });
                //e.Transform.Scale *= 1/4f;
            }
            
            {
                var e = Engine.Entities.CreateEntity("crate_cloud2");
                e.AddComponent(new ParallaxScrollingComponent(e, Engine.Textures["crate"])
                {
                    Layer = DrawingLayer.Background2,
                    ParallaxRatio = -1.2f
                });
                e.AddComponent(new AutoMovingComponent(e) { Speed = -10});
                e.Transform.Position = new Vector2(-170, -180);
                //e.Transform.Scale *= 1/4f;
            }
            
            Entity player = CreatePlayer(new Vector2(-350, 30));

            CreateCrate(new Vector2(-212, 70), "platform", 64, 64, BodyType.Static, Color.LightBlue, 0);
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
                //string name = mod == 1 ? "ground" : "platform";
                string name = "platform";

                //BodyType bodyType = mod == 1 ? BodyType.Dynamic : BodyType.Static;
                BodyType bodyType = BodyType.Static;

                
                CreateCrate(lastPos , name, 64, 64, bodyType);

                lastPos.X += cubeWidth;// + 5;
                //lastPos.Y += cubeHeight + 5;
            }
        }

        private Entity CreatePlayer(Vector2 pos)
        {
            Entity player = Engine.Entities.CreateEntity("player");

            //player.Transform.Scale = new Vector2(0.5f, 1f);
            player.Transform.Position = pos;

            const int playerWidth = 64; //96;
            const int playerHeight = 64; //96;

            AnimatedSpriteComponent animationComponent = new AnimatedSpriteComponent(player, Engine.Textures["lumberjack"]);
            animationComponent.AddAnimation("walk_left", new SpriteAnimation(64, 64, 4, 256, 64, 0, 64));
            animationComponent.AddAnimation("walk_right", new SpriteAnimation(64, 64, 4, 256, 64, 0, 128));
            animationComponent.AddAnimation("walk_up", new SpriteAnimation(64, 64, 4, 256, 64, 0, 196));
            animationComponent.AddAnimation("walk_down", new SpriteAnimation(64, 64, 4, 256, 64, 0, 0));
            animationComponent.SetCurrentAnimation("walk_right");

            player.AddComponent(animationComponent);
            //player.AddComponent(new SpriteComponent(player, Engine.Textures["crate"]));
            player.AddComponent(new PlayerInput(player));

            //Body body = BodyFactory.CreateRectangle(
            //    Engine.Entities.PhysicsSystem.World,
            //    ConvertUnits.ToSimUnits(playerWidth/2 * player.Transform.Scale.X),
            //    ConvertUnits.ToSimUnits(playerHeight * player.Transform.Scale.Y),
            //    1);

            //body.BodyType = BodyType.Dynamic;
            //body.FixedRotation = true;
            //body.Mass = 0;
            //body.Friction = 1f;
            //body.LinearDamping = 1f;

            //body.Position = new Vector2(
            //    ConvertUnits.ToSimUnits(player.Transform.Position.X),
            //    ConvertUnits.ToSimUnits(player.Transform.Position.Y));

            //body.Rotation = player.Transform.Rotation;
            //BodyComponent bodyComponent = new BodyComponent(player, body);

            //bodyComponent.OnCollision += (sender, args) =>
            //{
            //    if (args.BodyComponentB.GameObject.Tag == "ground")
            //        args.BodyComponentB.Body.IgnoreGravity = false;
            //};

            //player.AddComponent(bodyComponent);

            //e.AddComponent(new RectCollider(e) { Width = width, Height = height });
            player.AddComponent(new RectRenderer(player, Rectangle.Empty, new Texture2D(Engine.Window.GraphicsDevice, 1, 1)));


            return player;
        }

        private static void CreateCrate(Vector2 pos, string name, int width = 64, int height = 64, BodyType bodyType = BodyType.Dynamic, Color? color = null, float friction = 0.2f)
        {
            const int crateWidth = 64;
            const int crateHeight = 64;

            var e = Engine.Entities.CreateEntity(name);
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

            e.AddComponent(new SpriteComponent(e, Engine.Textures["crate"]) { Color = c });
        }

        protected override void UnloadContent()
        {
            Engine.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(gameTime);

            //TODO: Move to a manager/renderer \ la SpriteBatchRenderer
            _primitiveBatch.Begin(PrimitiveType.LineList, Engine.Cameras.Current.TransformMatrix);

            _primitiveBatch.AddVertex(new Vector2(0, 0), Color.Red);
            _primitiveBatch.AddVertex(new Vector2(100, 100), Color.Red);

            _primitiveBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
