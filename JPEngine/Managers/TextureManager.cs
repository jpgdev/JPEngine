using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JPEngine.Managers
{
    public class TextureManager : ResourceManager<Texture2D>
    {
        internal TextureManager(ContentManager content)
            : base(content)
        {
        }
    }
}