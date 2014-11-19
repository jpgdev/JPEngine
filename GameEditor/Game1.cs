using System.Threading;
using JPEngine;
using JPEngine.Components;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEditor
{
    public class Game1 : GameControl
    {
        protected override void Initialize()
        {
            base.Initialize();

            Engine.Initialize((IGraphicsDeviceService)Services.GetService(typeof(IGraphicsDeviceService)), TopLevelControl.Handle);

            Entity mainCamera = new Entity("_MainCamera", true);

            mainCamera.AddComponent(new CameraComponent(mainCamera));
            //mainCamera.AddComponent(new CameraInput(mainCamera));
            Engine.Cameras.SetCurrent(mainCamera.GetComponent<CameraComponent>());


            Engine.Textures.Add("crate", "Sprites/crate", true);
            Engine.Textures.Add("grass", "Tiles/grass", true);
            Engine.Textures.Add("background", "Tiles/background", true);

            Engine.SoundFX.Add("ammo_pickup", "Sounds/ammo_pickup", true);

            InitTestEntities();
        }

        private static void InitTestEntities()
        {
            var e = new Entity("player");
            //e.Transform.Scale = new Vector2(0.5f, 0.5f);

            e.AddComponent(new SpriteComponent(e, Engine.Textures["crate"]));
            //e.AddComponent(new PlayerInput(e));
            e.AddComponent(new RectCollider(e) {Width = 96, Height = 96});
            e.AddComponent(new RectRenderer(e, Rectangle.Empty, new Texture2D(Engine.Window.GraphicsDevice, 1, 1)));

            Engine.Entities.AddEntity(e);
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!this.Focused)
            {
                Thread.Sleep(50);
            }
            else
            {
                Thread.Sleep(1);
            }

            this.GraphicsDevice.Clear(Color.Blue);
            Engine.Draw(gameTime);
        }
    }
}
