using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public interface IWindowManager
    {
        GraphicsDevice GraphicsDevice { get; }

        int Width { get; set; }

        int Height { get; set; }

        bool IsFullScreen { get; set; }

        Viewport Viewport { get; }
    }
}