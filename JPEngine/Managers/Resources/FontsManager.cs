using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class FontsManager : ResourceManager<SpriteFont>
    {
        internal FontsManager(ContentManager content) 
            : base(content)
        {
        }
    }
}
