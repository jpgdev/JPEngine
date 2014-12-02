using Microsoft.Xna.Framework;

namespace JPEngine.Graphics
{
    public interface ISprite<out TTextureType> : IRenderable
    {
        TTextureType Texture { get; }

        Rectangle Bounds { get; }

        Rectangle? DrawnPortion { get; }
    }
}
