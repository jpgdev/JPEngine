using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public interface IWindowManager
    {
        GraphicsDevice GraphicsDevice { get; }

        int Width { get; set; }

        int Height { get; set; }

        bool IsFullScreen { get; set; }

        bool IsMouseVisible { get; set; }

        Rectangle Bounds { get; }

        Viewport Viewport { get; }
    }
}