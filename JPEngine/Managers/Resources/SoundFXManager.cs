using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace JPEngine.Managers
{
    public class SoundFXManager : ResourceManager<SoundEffect>
    {
        internal SoundFXManager(ContentManager content)
            : base(content)
        {
        }

    }
}