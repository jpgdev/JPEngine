#region Using Statements
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
#endregion

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


            base.Initialize();
        }

        protected override void LoadContent()
        {           
            Engine.LoadContent(Content);

            Engine.TextureManager.Add("crate", "Sprites/crate", true);
            Engine.TextureManager.Add("grass", "Tiles/grass", true);

            Engine.SoundManager.Add("ammo_pickup", "Sounds/ammo_pickup", true);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            Engine.Draw();

            //todo: Put in the SpriteManager
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D text = Engine.TextureManager.GetResource("crate");//Engine.TextureManager.Load("crate");

            spriteBatch.Begin();
            spriteBatch.Draw(text, new Vector2(10, 10), Color.White);
            spriteBatch.End();
            ////

            base.Draw(gameTime);
        }
    }
}
