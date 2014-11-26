using JPEngine.Graphics;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface ISpriteRenderer : IManager
    {
        void Begin(Matrix? transformMatrix = null);

        void Draw(ISprite sprite);

        void DrawString(IText text);

        void End();
    }
}
