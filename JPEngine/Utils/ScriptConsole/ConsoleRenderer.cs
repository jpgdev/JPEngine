using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Utils.ScriptConsole
{
    public class ConsoleRenderer
    {

        private enum State
        {
            Opened,
            Opening,
            Closed,
            Closing
        }

        private Texture2D _pixel;
        private Color _backgroundColor = new Color(0, 0, 0, 125);


        public Rectangle Bounds { get; set; }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }
        

        internal ConsoleRenderer(Game game)
        {
            _pixel = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _pixel.SetData(new[] { Color.White });


            Bounds = new Rectangle(0,0, game.GraphicsDevice.Viewport.Width, 300);

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_pixel, Bounds, _backgroundColor);

            spriteBatch.End();
        }
    }
}
