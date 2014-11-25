using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{

    public interface IText : IRenderable
    {
        SpriteFont Font { get; }

        string Text { get; }
    }
}
