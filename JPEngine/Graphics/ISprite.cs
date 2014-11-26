using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{
    public interface ISprite : IRenderable
    {
        Texture2D Texture { get; }

        Rectangle Bounds { get; }

        Rectangle? DrawnPortion { get; }
    }
}
