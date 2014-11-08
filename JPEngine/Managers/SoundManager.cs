using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    public class SoundManager : ResourceManager<SoundEffect>, IAudioManager
    {


#region Attributes

        private Dictionary<string, SoundEffectInstance> _soundInstances;

#endregion

        internal SoundManager(ContentManager content)
            : base(content)
        {
            _soundInstances = new Dictionary<string, SoundEffectInstance>();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Play(string name)
        {
            throw new NotImplementedException();
        }

        public void Stop(string name)
        {
            throw new NotImplementedException();
        }
    }
}
