using JPEngine;
using JPEngine.ECS;
using JPEngine.ECS.Components;
using JPEngine.Managers;
using JPEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CustomGame
{
    public class Game1 : GameControl
    {
        protected override void Initialize()
        {
            base.Initialize();
            
            Engine.Initialize((GraphicsDeviceService)Services.GetService(typeof(GraphicsDeviceService)), Handle);

            Entity mainCamera = new Entity("_MainCamera", true);

            mainCamera.AddComponent(new CameraComponent(mainCamera));
            //mainCamera.AddComponent(new CameraInput(mainCamera));
            Engine.Cameras.SetCurrent(mainCamera.GetComponent<CameraComponent>());


            Engine.Textures.Add("crate", "Sprites/crate", true);
            Engine.Textures.Add("grass", "Tiles/grass", true);
            Engine.Textures.Add("background", "Tiles/background", true);

            Engine.SoundFX.Add("ammo_pickup", "Sounds/ammo_pickup", true);

            {
                var e = new Entity("player");
                //e.Transform.Scale = new Vector2(0.5f, 0.5f);

                e.AddComponent(new DrawableSpriteComponent(e, Engine.Textures["crate"]));
                //e.AddComponent(new PlayerInput(e));
                e.AddComponent(new RectCollider(e) { Width = 96, Height = 96 });
                e.AddComponent(new RectRenderer(e, Rectangle.Empty, new Texture2D(Engine.Window.GraphicsDevice, 1, 1)));

                Engine.Entities.AddEntity(e);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(gameTime);
        }
    }
}
