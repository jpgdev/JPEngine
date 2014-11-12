﻿using System.Reflection;
using JPEngine.ECS;
using NLua;

namespace JPEngine.Utils.ScriptConsole
{
    public class LuaParser : IScriptParser
    {
        private readonly Lua _luaParser;

        public LuaParser()
        {
            _luaParser = new Lua();
        }

        public void Initialize()
        {
            _luaParser.LoadCLRPackage();

            _luaParser.RegisterFunction("byTag", Engine.Entities, typeof (EntitiesManager).GetMethod("GetEntityByTag"));
            _luaParser.RegisterFunction("byTags", Engine.Entities, typeof (EntitiesManager).GetMethod("GetEntitiesByTag"));
            _luaParser.RegisterFunction("init", this,
                typeof (LuaParser).GetMethod("SetupBasicVariables", BindingFlags.NonPublic | BindingFlags.Instance));
        }

        public object[] ParseScript(string script)
        {
            return _luaParser.DoString(script);
        }

        public object[] ParseFile(string path)
        {
            return _luaParser.DoFile(path);
        }

        private void SetupBasicVariables()
        {
            _luaParser.DoString(
                "import ('JPEngine')\n" +
                "import ('JPEngine.Managers')\n" +
                "import ('Microsoft.Xna.Framework')\n" +
                "window = Engine.Window\n" +
                "entities = Engine.Entities\n" +
                "sounds = Engine.SoundFX\n" +
                "musics = Engine.Music\n" +
                //"cameras = Engine.Cameras\n" +
                "player = entities:GetEntitiesByTag('player')[0]");
        }
    }
}