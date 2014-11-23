using System;
using System.Collections.Generic;

namespace JPEngine.Managers
{
    public class SettingsManager : Manager, ISettingsManager
    {
        //TODO: Link MusicManager, SoundFXManager, WindowManager etc... to a setting?
        //      example: Settings.Add(new Setting("soundfx_volume", 0.5f);
        //               SoundFXManager.GlobalVolumeSettingsKey = "soundfx_volume";
        //TODO: Add a Folder/Category system

        private Dictionary<string, Setting> _settings;

        internal SettingsManager()
        {
            _settings = new Dictionary<string, Setting>();
        }

        public Setting this[string name]
        {
            get { return Get(name); }
        }

        public bool Add(Setting setting)
        {
            if (!_settings.ContainsKey(setting.Name))
            {
                _settings.Add(setting.Name, setting);
                return true;
            }

            return false;
        }

        public bool Remove(string name)
        {
            return _settings.Remove(name);
        }

        public Setting Get(string name)
        {
            return !_settings.ContainsKey(name) ? null : _settings[name];
        }

        public bool Save(string path)
        {
            //path = @"C:\Users\JP\Desktop\test\settings.txt";
            //Utils.JsonHelper.SaveToFile(_settings, path);
            throw new NotImplementedException();
        }

        public bool Load(string path)
        {
            //path = @"C:\Users\JP\Desktop\test\settings.txt";
            //_settings.Clear();
            //_settings = Utils.JsonHelper.LoadFromFile<Dictionary<string, Setting>>(path);
            throw new NotImplementedException();
        }       
    }
}