using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public interface IWindowManager : IManager
    {
        GraphicsDevice GraphicsDevice { get; }

        int Width { get; set; }

        int Height { get; set; }

        //TODO: Add GameWidth, GameHeight & ScreenWidth, ScreenHeight
        //Game____ is the Viewport, while the Screen_____ is the Bounds mostly.

        bool IsFullScreen { get; set; }

        bool IsMouseVisible { get; set; }

        Rectangle Bounds { get; }

        Viewport Viewport { get; }
    }
}