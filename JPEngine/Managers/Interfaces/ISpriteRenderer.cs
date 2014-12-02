using JPEngine.Graphics;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface ISpriteRenderer<in TTextureType, in TFontType> : IManager
    {
        void Begin(Matrix? transformMatrix = null);

        void Draw(ISprite<TTextureType> sprite);

        void DrawString(IText<TFontType> text);

        void End();
    }
}
