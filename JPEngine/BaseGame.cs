using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace JPEngine
{
    public abstract class BaseGame
    {
        protected BaseGame()
        {
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Start()
        {
            Stopwatch _timer = Stopwatch.StartNew();
            TimeSpan _elapsed = new TimeSpan(0);

            Initialize();
            LoadContent();

            while (true)
            {
                GameTime _gameTime = new GameTime(_timer.Elapsed, _timer.Elapsed - _elapsed);
                _elapsed = _timer.Elapsed;

                //Update the engine & game
                Engine.Update(_gameTime);
                Update(_gameTime);

                //Draw the engine & game
                Engine.Draw(_gameTime);
                Draw(_gameTime);
            }
        }

        /// <summary>
        /// Initialize the game before starting it.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Load the content before the game is started.
        /// </summary>
        protected abstract void LoadContent();

        /// <summary>
        /// Executing each frame.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Update(GameTime gameTime);

        /// <summary>
        /// Executing each frame to draw the game.
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Draw(GameTime gameTime);


       
    }
}
