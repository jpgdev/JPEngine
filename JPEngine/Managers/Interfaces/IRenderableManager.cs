
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface IRenderableManager : IManager
    {
        void Draw(GameTime gameTime);
    }
}
