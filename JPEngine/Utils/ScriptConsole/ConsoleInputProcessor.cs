using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JPEngine.Events;
using JPEngine.Managers;
using JPEngine.Managers.Input;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Utils.ScriptConsole
{
    internal class ConsoleInputProcessor
    {
        private readonly ConsoleOptions _options;

        internal bool IsActive { get; private set; }

        internal CommandHistory History { get; private set; }

        internal InputBuffer Buffer { get; private set; }

        internal List<OutputLine> Out { get; set; }

        internal IScriptParser ScriptParser { get; set; }
        
        internal event EventHandler Open;
        internal event EventHandler Close;

        internal ConsoleInputProcessor(ConsoleOptions consoleOptions)
        {
            _options = consoleOptions;

            ScriptParser = new LuaParser();
            ScriptParser.Initialize();

            History = new CommandHistory();
            Out = new List<OutputLine>();
            Buffer = new InputBuffer();

            Engine.Input.Keyboard.KeyClicked += EventInput_KeyClicked;
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

            //Special commands using the CTRL key
            if (e.Control)
            {
                switch (e.Key)
                {
                    case Keys.A:
                        Buffer.IsSelecting = true;
                        Buffer.CursorPosition = 0;
                        Buffer.CursorPosition = Buffer.Value.Length;
                        return;

                    case Keys.C:
                        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) //Thread Apartment must be in Single-Threaded for the Clipboard to work
                            System.Windows.Forms.Clipboard.SetText(Buffer.Selection);
                        return;

                    case Keys.X:
                        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) //Thread Apartment must be in Single-Threaded for the Clipboard to work
                        {
                            System.Windows.Forms.Clipboard.SetText(Buffer.Selection);
                            Buffer.ReplaceSelection(string.Empty);
                        }
                        return;

                    case Keys.V:
                        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA) //Thread Apartment must be in Single-Threaded for the Clipboard to work
                            AddToInputBuffer(System.Windows.Forms.Clipboard.GetText());
                        return;
                }
            }
            
            switch (e.Key)
            {
                case Keys.Enter: 
                    ExecuteBuffer(); 
                    break;

                case Keys.Back:
                    if (Buffer.SelectionLength > 0)
                        Buffer.ReplaceSelection(string.Empty);
                    else
                        Buffer.RemoveBeforeCursor(1);

                    break;

                case Keys.Delete:
                    if (Buffer.SelectionLength > 0)
                        Buffer.ReplaceSelection(string.Empty);
                    else
                        Buffer.RemoveAfterCursor(1);

                    break;

                case Keys.Up: 
                    Buffer.Set(History.Previous());
                    break;

                case Keys.Down:
                    Buffer.Set(History.Next());
                    break;

                case Keys.Left:
                    Buffer.IsSelecting = e.Shift;
                    Buffer.CursorPosition--;
                    break;

                case Keys.Right:
                    Buffer.IsSelecting = e.Shift;
                    Buffer.CursorPosition++;
                    break;
                    
                default:
                    Buffer.IsSelecting = e.Shift;
                    char c = KeyboardHelper.GetCharValue(e.Key, e.Shift);
                    if (IsPrintable(c))
                    {
                        if (Buffer.SelectionLength > 0)
                            Buffer.ReplaceSelection(c.ToString());
                        else
                            Buffer.Add(c.ToString());
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

        internal void AddToInputBuffer(string text)
        {
            var lines = text.Split('\n').Where(line => line != "").ToArray();
            int i;
            for (i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                Buffer.Add(line);
                ExecuteBuffer();
            }
            Buffer.Add(lines[i]);
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
            if (string.IsNullOrEmpty(Buffer.Value))
                return;
            
            try
            {
                Out.Add(new OutputLine(Buffer.Value, OutputLineType.Command));
                object[] result = ScriptParser.ParseScript(Buffer.Value);
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

            History.Add(Buffer.Value);
            Buffer.Clear();
        }
    }
}
