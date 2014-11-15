using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace JPEngine.Managers
{
    public class SoundFXManager : ResourceManager<SoundEffect>
    {
        //TODO: Enable a way to have multiple instances of the same sound
        // Maybe return the SoundInstance in the Play method?

        //private readonly Dictionary<string, SoundEffectInstance> _soundInstances;

        internal SoundFXManager(ContentManager content)
            : base(content)
        {
            //_soundInstances = new Dictionary<string, SoundEffectInstance>();
        }

        //public bool Play(string name, float volume = 1f)
        //{
        //    if (IsResourceLoaded(name))
        //    {
        //        _soundInstances[name] = _resources[name].CreateInstance();
        //        _soundInstances[name].Volume = volume;
        //        _soundInstances[name].Play();

        //        return true;
        //    }
        //    throw new Exception(string.Format("The sound effect '{0}' is not loaded.", name));
        //}

        //public bool Stop(string name)
        //{
        //    if (_soundInstances.ContainsKey(name))
        //    {
        //        _soundInstances[name].Stop(true);
        //        _soundInstances.Remove(name);
        //        return true;
        //    }

        //    return false;
        //}
    }
}