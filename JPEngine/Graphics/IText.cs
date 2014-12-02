using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Graphics
{

    public interface IText<out TFontType> : IRenderable
    {
        TFontType Font { get; }

        string Text { get; }
    }
}
