using System.Reflection;
using JPEngine.Entities;
using Microsoft.Xna.Framework;
using NLua;

namespace JPEngine.Utils.ScriptConsole
{
    public class LuaParser : IScriptParser
    {
        private readonly Lua _luaParser;

        public Lua Parser
        {
            get { return _luaParser; }
        }

        public LuaParser()
        {
            _luaParser = new Lua();
            _luaParser.LoadCLRPackage();
        }

        public void Initialize()
        {
            _luaParser.RegisterFunction("byTag", Engine.Entities, typeof (EntitiesManager).GetMethod("GetEntityByTag"));
            _luaParser.RegisterFunction("byTags", Engine.Entities, typeof (EntitiesManager).GetMethod("GetEntitiesByTag"));
            
            //_luaParser.RegisterFunction("byType", Engine.Entities, typeof(EntitiesManager).GetMethod("GetAllComponentsOfType", 
            //    BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Type) }, null));

            _luaParser.RegisterFunction("init", this,
                typeof (LuaParser).GetMethod("SetupBasicVariables", BindingFlags.NonPublic | BindingFlags.Instance));

            InitializeXnaCtors();
        }

        private void InitializeXnaCtors()
        {
            //Vector2
            _luaParser.RegisterFunction("vector2", 
                typeof (Vector2).GetConstructor(new[] {typeof (float), typeof (float)}));

            //Color
            _luaParser.RegisterFunction("color", 
                typeof (Color).GetConstructor(new[] {typeof (int), typeof (int), typeof (int)}));
            _luaParser.RegisterFunction("colora",
                typeof (Color).GetConstructor(new[] {typeof (int), typeof (int), typeof (int), typeof (int)}));
            _luaParser.RegisterFunction("colorf",
                typeof (Color).GetConstructor(new[] {typeof (float), typeof (float), typeof (float)}));
            _luaParser.RegisterFunction("colorfa",
                typeof (Color).GetConstructor(new[] {typeof (float), typeof (float), typeof (float), typeof (float)}));
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
                "cameras = Engine.Cameras\n" +
                "player = entities:GetEntitiesByTag('player')[0]");
        }

        //public void AddCommand(string name, object behavior)
        //{
        //    MethodBase method = behavior as MethodBase;

        //    if (method == null)
        //        throw new ArgumentException("The behavior should be of type MethodBase.");

        //    _luaParser.RegisterFunction(name, method);
        //}

        //public void AddCommand(string name, object target, object behavior)
        //{
        //    MethodBase method = behavior as MethodBase;

        //    if(method  == null)
        //        throw new ArgumentException("The behavior should be of type MethodBase.");

        //    _luaParser.RegisterFunction(name, target, method);
        //}
    }
}