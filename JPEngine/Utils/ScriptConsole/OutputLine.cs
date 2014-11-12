namespace JPEngine.Utils.ScriptConsole
{
    public enum OutputLineType
    {
        Command,
        Output
    }

    public class OutputLine
    {
        public OutputLine(string output, OutputLineType type)
        {
            Output = output;
            Type = type;
        }

        public string Output { get; set; }
        public OutputLineType Type { get; set; }

        public override string ToString()
        {
            return Output;
        }
    }
}