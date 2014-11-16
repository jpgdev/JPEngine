using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JPEngine.Events;
using JPEngine.Managers;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Utils.ScriptConsole
{
    internal class ConsoleInputProcessor
    {
        private readonly ConsoleOptions _options;
        
        internal bool IsActive { get; private set; }

        internal CommandHistory History { get; private set; }

        internal OutputLine Buffer { get; private set; }

        internal List<OutputLine> Out { get; set; }

        internal IScriptParser ScriptParser { get; set; }
        
        public event EventHandler Open;
        public event EventHandler Close;

        internal ConsoleInputProcessor(ConsoleOptions consoleOptions)
        {
            _options = consoleOptions;

            ScriptParser = new LuaParser();
            ScriptParser.Initialize();

            History = new CommandHistory();
            Out = new List<OutputLine>();
            Buffer = new OutputLine("", OutputLineType.Command);

            Engine.Input.KeyClicked += EventInput_KeyClicked;
        }

        private void EventInput_KeyClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == _options.ToggleKey)
            {
                ToggleConsole();
                return;
            }

            if (!IsActive)
                return;

            if (e.Key == Keys.V && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) // CTRL + V
            {
                if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) //Thread Apartment must be in Single-Threaded for the Clipboard to work
                {
                    AddToBuffer(System.Windows.Forms.Clipboard.GetText());
                    return;
                }
            }

            switch (e.Key)
            {
                case Keys.Enter: ExecuteBuffer(); break;

                case Keys.Back:
                    if (Buffer.Output.Length > 0)
                    {
                        Buffer.Output = Buffer.Output.Substring(0, Buffer.Output.Length - 1);
                    }
                    break;

                case Keys.Up: Buffer.Output = History.Previous(); break;

                case Keys.Down: Buffer.Output = History.Next(); break;
                    
                default:
                    char c = InputManager.GetCharValue(e.Key, e.Shift);
                    if (IsPrintable(c))
                    {
                        Buffer.Output += c;
                    }
                    break;
            }
        }

        internal void ToggleConsole()
        {
            IsActive = !IsActive;

            if (IsActive && Open != null) 
                Open(this, EventArgs.Empty);

            if (!IsActive && Close != null) 
               Close(this, EventArgs.Empty);
        }

        internal void AddToBuffer(string text)
        {
            var lines = text.Split('\n').Where(line => line != "").ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                Buffer.Output += line;
                ExecuteBuffer();
            }
            Buffer.Output += lines[i];
        }

        internal void AddToOutput(string text)
        {
            if (_options.OpenOnWrite)
            {
                IsActive = true;
                if (Open != null)
                    Open(this, EventArgs.Empty);
            }

            foreach (var line in text.Split('\n'))
            {
                Out.Add(new OutputLine(line, OutputLineType.Output));
            }
        }


        private bool IsPrintable(char letter)
        {
            return _options.Font.Characters.Contains(letter);
        }

        private void ExecuteBuffer()
        {
            if (string.IsNullOrEmpty(Buffer.Output))
                return;
            
            try
            {
                Out.Add(new OutputLine(Buffer.Output, OutputLineType.Command));
                object[] result = ScriptParser.ParseScript(Buffer.Output);
                if (result != null)
                {
                    foreach (var line in result)
                    {
                        string output = "null";
                        if (line != null)
                            output = line.ToString();

                        Out.Add(new OutputLine(output, OutputLineType.Output));
                    }
                }
            }
            catch (Exception e)
            {
                string message = string.Format("Exception: {0}; Inner Exception: {1} ", e.Message, e.InnerException != null ? e.InnerException.Message : "");
                Out.Add(new OutputLine(message, OutputLineType.Output));
            }

            History.Add(Buffer.Output);
            Buffer.Output = "";
        }
    }
}
