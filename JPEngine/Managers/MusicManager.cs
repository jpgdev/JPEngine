using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace JPEngine.Managers
{
    public class MusicManager : ResourceManager<Song>, IAudioManager
    {


#region Attributes

        private SongCollection _songs;

#endregion

        internal MusicManager(ContentManager content)
            : base(content)
        {
            
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
