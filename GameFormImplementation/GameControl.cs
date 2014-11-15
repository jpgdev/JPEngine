using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace CustomGame
{
    public abstract class GameControl : GraphicsDeviceControl
    {
        private GameTime _gameTime;
        private Stopwatch _timer;
        private TimeSpan _elapsed;

        protected override void Initialize()
        {
            _timer = Stopwatch.StartNew();

            Application.Idle += delegate { GameLoop(); };
        }

        protected override void Draw()
        {
            Draw(_gameTime);
        }

        private void GameLoop()
        {
            _gameTime = new GameTime(_timer.Elapsed, _timer.Elapsed - _elapsed);
            _elapsed = _timer.Elapsed;

            Update(_gameTime);
            Invalidate();
        }

        protected abstract void Update(GameTime gameTime);

        protected abstract void Draw(GameTime gameTime);
    }
}
