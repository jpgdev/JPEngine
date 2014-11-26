
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface IRenderableManager : IUpdateableManager
    {
        void Draw(GameTime gameTime);
    }
}
