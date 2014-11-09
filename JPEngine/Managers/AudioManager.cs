//using Microsoft.Xna.Framework.Content;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JPEngine.Managers
//{
//    public class AudioManager 
//    {
//        public enum AudioType
//        {
//            Music,
//            SoundFX
//        }

//        private SoundFXManager _soundFXManager;
//        private MusicManager _musicManager;


//        public SoundFXManager SoundFX
//        {
//            get { return _soundFXManager; }
//        }

//        public MusicManager Music
//        {
//            get { return _musicManager; }
//        }


//        internal AudioManager(ContentManager content)
//        {
//            _soundFXManager = new SoundFXManager(content);
//            _musicManager = new MusicManager(content);
//        }

//        //public bool Play(string name)
//        //{
//        //    if (_soundFXManager.IsLoaded(name))
//        //        return Play(name, AudioType.SoundFX);
//        //    else if (_musicManager.IsLoaded(name))
//        //        Play(name, AudioType.Music);

//        //    return false;
//        //}



//        //public bool Play(string name, AudioType audioType, float volume = 1f)
//        //{
//        //    switch(audioType)
//        //    {
//        //        case AudioType.Music:
//        //            return _musicManager.Play(name, volume);
//        //        case AudioType.SoundFX:
//        //            return _soundFXManager.Play(name, volume);
//        //    }

//        //    return false;
//        //}

//        //public bool Stop(string name, AudioType audioType)
//        //{
//        //    switch (audioType)
//        //    {
//        //        case AudioType.Music:
//        //            return _musicManager.Stop();
//        //        case AudioType.SoundFX:
//        //            return _soundFXManager.Stop(name);
//        //    }

//        //    return false;
//        //}
//    }
//}
