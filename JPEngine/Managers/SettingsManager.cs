using System;
using System.Collections.Generic;

namespace JPEngine.Managers
{
    public class SettingsManager : Manager
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
            if (!_settings.ContainsKey(name))
                throw new Exception(string.Format("The setting '{0}' does not exist.", name));

            return _settings[name];
        }

        public void SaveSettings()
        {
            Utils.JsonHelper.SaveToFile(_settings, @"C:\Users\JP\Desktop\test\settings.txt");
        }

        public void LoadSettings()
        {
            //_settings.Clear();
            _settings = Utils.JsonHelper.LoadFromFile<Dictionary<string, Setting>>(@"C:\Users\JP\Desktop\test\settings.txt");
        }       
    }
}