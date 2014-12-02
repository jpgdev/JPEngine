namespace JPEngine.Utils.ScriptConsole
{
    public interface IScriptParser
    {
        void Initialize();

        object[] ParseScript(string script);

        object[] ParseFile(string path);

        //TODO: Overkill?
        //void AddCommand(string name, object behavior);
        //TODO: Overkill?
        //void AddCommand(string name, object target, object behavior);
    }
}
