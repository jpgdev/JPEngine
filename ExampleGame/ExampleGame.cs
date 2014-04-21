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
#endregion

namespace ExampleGame
{

    public class ExampleGame : Game
    {
        GraphicsDeviceManager graphics;        
        Engine engine;

        public ExampleGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            engine = new Engine();
            engine.Initialize();
            




            base.Initialize();
        }

        protected override void LoadContent()
        {           
            engine.LoadContent(Content);



        }

        protected override void UnloadContent(){ }

        protected override void Update(GameTime gameTime)
        {
            engine.Update(gameTime);




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            engine.Draw();
            


            base.Draw(gameTime);
        }
    }
}
