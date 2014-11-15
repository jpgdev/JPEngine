using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JPEngine.Events;
using JPEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Utils.ScriptConsole
{
    //TODO: Check if null on each option?
    public class ConsoleOptions
    {
        public SpriteFont Font { get; set; }
        public Keys ToggleKey { get; set; }

        public bool OpenOnWrite { get; set; }
        public bool PauseGameWhenOpened { get; set; }

        public float CursorBlinkSpeed { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Padding { get; set; }
        public int Margin { get; set; }
        

        public string Cursor { get; set; }
        public string Prompt { get; set; }


        public Color PromptColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color BufferColor { get; set; }
        public Color CursorColor { get; set; }
        public Color PastCommandColor { get; set; }
        public Color PastCommandOutputColor { get; set; }

        //TODO: ScrollUpKey, ScrollDownKey, IsScrollable, IsClickable (Mouse) (if not, the mouse will ignore it)

        public ConsoleOptions(SpriteFont font)
        {
            //TODO: Find a way to have a Font in the Engine, without relying on the Game
            if (font == null)
                throw new ArgumentNullException("The font cannot be null.");

            Font = font;

            ToggleKey = Keys.F1;

            OpenOnWrite = true;
            PauseGameWhenOpened = true;

            Width = 500;
            Height = 300;

            Padding = 20;
            Margin = 20;

            CursorBlinkSpeed = 0.5f;

            Prompt = ">";
            Cursor = "_";

            PromptColor = Color.White;
            BufferColor = Color.Gold;
            CursorColor = Color.OrangeRed;
            PastCommandColor = Color.Aqua;
            PastCommandOutputColor = Color.Violet;
            BackgroundColor = new Color(0, 0, 0, 175);
        }
    }

    public class ScriptConsole
    {
        private bool _isActive;
        //private bool _isHandled;

        private IScriptParser _scriptParser;

        public ConsoleOptions Options { get; private set; }

        public event EventHandler Open;
        public event EventHandler Close;

        //public Keys ToggleKey { get; set; }
        public bool IsActive { get { return _isActive; } }
        public CommandHistory History { get; private set; }
        public OutputLine Buffer { get; private set; }
        public List<OutputLine> Out { get; set; }

        public IScriptParser ScriptParser
        {
            get { return _scriptParser; }
            set
            {
                if(value != null )
                    _scriptParser = value;
            }
        }
        
        #region Rendering Variables

        //TODO: Refactor out of here
        
        private Texture2D _pixel;
        private float _oneCharacterWidth;
        private int _maxCharactersPerLine;
        private Vector2 _position;
        private Vector2 _firstCommandPositionOffset = Vector2.Zero;

        Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, Options.Width - (Options.Margin * 2), Options.Height);
            }
        }

        Rectangle InnerBounds
        {
            get
            {
                return new Rectangle(Bounds.X + Options.Padding, Bounds.Y + Options.Padding, Bounds.Width - Options.Padding, Bounds.Height);
            }
        }

        private Vector2 FirstCommandPosition
        {
            get
            {
                return new Vector2(InnerBounds.X, InnerBounds.Y) + _firstCommandPositionOffset;
            }
        }

        #endregion

        public ScriptConsole(ConsoleOptions options) //  SpriteBatch spriteBatch, 
        {
            //if(game == null)
            //    throw new ArgumentNullException("The game cannot be null.");

            if (options == null)
                throw new ArgumentNullException("The options cannot be null.");

            Options = options;

            _scriptParser = new LuaParser();

            History = new CommandHistory();
            Out = new List<OutputLine>();
            Buffer = new OutputLine("", OutputLineType.Command);

            //Renderer
            _pixel = new Texture2D(Engine.Window.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
            
            _position = new Vector2(Options.Margin, 0);

            _oneCharacterWidth = Options.Font.MeasureString("x").X;
            _maxCharactersPerLine = (int)((Bounds.Width - Options.Padding * 2) / _oneCharacterWidth);
            //

            Engine.Input.KeyClicked += EventInput_KeyClicked;

            _scriptParser.Initialize();
        }

        public void AddToBuffer(string text)
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

        public void AddToOutput(string text)
        {
            if (Options.OpenOnWrite)
            {
                _isActive = true;
                if(Open != null)
                    Open(this, EventArgs.Empty);
            }

            foreach (var line in text.Split('\n'))
            {
                Out.Add(new OutputLine(line, OutputLineType.Output));
            }
        }


        private bool IsPrintable(char letter)
        {
            return Options.Font.Characters.Contains(letter);
        }

        private void ExecuteBuffer()
        {
            if (string.IsNullOrEmpty(Buffer.Output)) 
                return;

            //var output = commandProcesser.Process(Buffer.Output).Split('\n').Where(l => l != "");
            try
            {
                Out.Add(new OutputLine(Buffer.Output, OutputLineType.Command));
                object[] result = _scriptParser.ParseScript(Buffer.Output);
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

        public void ToggleConsole()
        {
            _isActive = !_isActive;

            if (_isActive)
                if(Open != null) Open(this, EventArgs.Empty);
            else
                if(Close != null) Close(this, EventArgs.Empty);

            //Console.WriteLine("Console now {0}", _isActive? "activated" : "desactivated");
        }

        private void EventInput_KeyClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Options.ToggleKey)
            {
                ToggleConsole();
                return;
                //_isHandled = true;
            }

            if (!_isActive) 
                return;

            //if (_isHandled || !_isActive)
            //{
            //    _isHandled = false;
            //    return;
            //}

            //History.Reset();

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
                case Keys.Enter:  ExecuteBuffer();  break;

                case Keys.Back:
                    if (Buffer.Output.Length > 0)
                    {
                        Buffer.Output = Buffer.Output.Substring(0, Buffer.Output.Length - 1);
                    }
                    break;

                case Keys.Up: Buffer.Output = History.Previous(); break;

                case Keys.Down: Buffer.Output = History.Next(); break;

                //case Keys.Tab: AutoComplete(); break;

                default:
                    char c = InputManager.GetCharValue(e.Key, e.Shift);
                    if (IsPrintable(c))
                    {
                        Buffer.Output += c;
                    }
                    break;
            }
        }


        #region Rendering Methods

        //TODO: Refactor out of here
        
        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_isActive) return;

            spriteBatch.Begin();
            spriteBatch.Draw(_pixel, Bounds, Options.BackgroundColor);

            Vector2 nextCommandPosition = DrawCommands(spriteBatch, Out, FirstCommandPosition);
            nextCommandPosition = DrawPrompt(spriteBatch, nextCommandPosition);
            var bufferPosition = DrawCommand(spriteBatch, Buffer.Output, nextCommandPosition, Options.BufferColor); //Draw the buffer
            DrawCursor(spriteBatch, bufferPosition, gameTime);

            spriteBatch.End();
        }

        private Vector2 DrawPrompt(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.DrawString(Options.Font, Options.Prompt, pos, Options.PromptColor);
            pos.X += _oneCharacterWidth * Options.Prompt.Length + _oneCharacterWidth;
            return pos;
        }

        private Vector2 DrawCommand(SpriteBatch spriteBatch, string command, Vector2 pos, Color color)
        {
            var splitLines = command.Length > _maxCharactersPerLine ? SplitCommand(command, _maxCharactersPerLine) : new[] { command };
            foreach (var line in splitLines)
            {
                if (IsInBounds(pos.Y))
                {
                    spriteBatch.DrawString(Options.Font, line, pos, color);
                }
                ValidateFirstCommandPosition(pos.Y + Options.Font.LineSpacing);
                pos.Y += Options.Font.LineSpacing;
            }
            return pos;
        }

        /// <summary>
        /// Draws the specified collection of commands and returns the position of the next command to be drawn
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        Vector2 DrawCommands(SpriteBatch spriteBatch, IEnumerable<OutputLine> lines, Vector2 pos)
        {
            var originalX = pos.X;
            foreach (var command in lines)
            {
                if (command.Type == OutputLineType.Command)
                {
                    pos = DrawPrompt(spriteBatch, pos);
                }
                //position.Y = DrawCommand(command.ToString(), position, GameConsoleOptions.Options.FontColor).Y;
                pos.Y = DrawCommand(spriteBatch, command.ToString(), pos, command.Type == OutputLineType.Command ? Options.PastCommandColor : Options.PastCommandOutputColor).Y;
                pos.X = originalX;
            }
            return pos;
        }

        private void DrawCursor(SpriteBatch spriteBatch, Vector2 pos, GameTime gameTime)
        {
            if (!IsInBounds(pos.Y))
            {
                return;
            }
            
            var split = SplitCommand(Buffer.ToString(), _maxCharactersPerLine).Last();
            pos.X += Options.Font.MeasureString(split).X;
            pos.Y -= Options.Font.LineSpacing;
            spriteBatch.DrawString(Options.Font,
                (int)(gameTime.TotalGameTime.TotalSeconds / Options.CursorBlinkSpeed) % 2 == 0
                    ? Options.Cursor
                    : "", pos, Options.CursorColor);
        }


        private static IEnumerable<string> SplitCommand(string command, int max)
        {
            var lines = new List<string>();
            while (command.Length > max)
            {
                var splitCommand = command.Substring(0, max);
                lines.Add(splitCommand);
                command = command.Substring(max, command.Length - max);
            }
            lines.Add(command);
            return lines;
        }
        
        void ValidateFirstCommandPosition(float nextCommandY)
        {
            if (!IsInBounds(nextCommandY))
            {
                _firstCommandPositionOffset.Y -= Options.Font.LineSpacing;
            }
        }

        bool IsInBounds(float yPosition)
        {
            return yPosition < Options.Height;
        }



        #endregion
        
    }
}
