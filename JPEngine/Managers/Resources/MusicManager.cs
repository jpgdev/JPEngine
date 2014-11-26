using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace JPEngine.Managers
{
    public class MusicManager : ResourceManager<Song>
    {
        internal MusicManager(ContentManager content)
            : base(content)
        {
        }

        //public bool Play(string name, float volume = 1f)
        //{
        //    if (IsResourceLoaded(name))
        //    {
        //        MediaPlayer.Volume = volume;
        //        MediaPlayer.IsRepeating = true;
        //        MediaPlayer.Play(_resources[name]);

        //        return true;
        //    }
        //    throw new Exception(string.Format("The sound effect '{0}' is not loaded.", name));
        //}

        //public bool Stop()
        //{
        //    MediaPlayer.Stop();

        //    return true;
        //}
    }
}