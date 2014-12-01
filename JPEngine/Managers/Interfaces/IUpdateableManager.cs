using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface IUpdateableManager : IManager
    {
        void Update(GameTime gameTime);
    }
}
