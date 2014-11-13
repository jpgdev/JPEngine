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
    public class ScriptConsole
    {
        private bool _isActive;
        //private bool _isHandled;
        //private Lua _luaParser;
        private readonly IScriptParser _scriptParser;

        public event EventHandler Open;
        public event EventHandler Close;

        public Keys ToggleKey { get; set; }
        public bool IsActive { get { return _isActive; } }
        public CommandHistory History { get; private set; }
        public OutputLine Buffer { get; private set; }
        public List<OutputLine> Out { get; set; }

        public bool OpenOnWrite { get; set; }

        public bool PauseGameWhenOpened { get; set; }


        #region Rendering Variables

        //TODO: Refactor out of here

        private readonly SpriteFont _font;
        private Texture2D _pixel;
        private float _oneCharacterWidth;
        private int _maxCharactersPerLine;
        private Vector2 _position;
        private int _width;
        private int _height;
        private Vector2 _firstCommandPositionOffset = Vector2.Zero;

        private string _prompt = ">";
        private Color _promptColor = Color.White;

        private Color _bufferColor = Color.Gold;

        private string _cursor = "_";
        private Color _cursorColor = Color.OrangeRed;
        private float _cursorBlinkSpeed = 0.5f;

        private Color _pastCommandColor = Color.Aqua;
        private Color _pastCommandOutputColor = Color.Violet;


        Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _width - (Margin * 2), _height);
            }
        }

        Rectangle InnerBounds
        {
            get
            {
                return new Rectangle(Bounds.X + Padding, Bounds.Y + Padding, Bounds.Width - Padding, Bounds.Height);
            }
        }

        private Vector2 FirstCommandPosition
        {
            get
            {
                return new Vector2(InnerBounds.X, InnerBounds.Y) + _firstCommandPositionOffset;
            }
        }

        public Color BackgroundColor { get; set; }

        public int Padding { get; set; }

        public int Margin { get; set; }

        #endregion

        

        //[DllImport("kernel32")]
        //static extern bool AllocConsole();

        internal ScriptConsole(Game game, SpriteFont font) //  SpriteBatch spriteBatch, 
        {
            _font = font;

            _scriptParser = new LuaParser();

            History = new CommandHistory();
            Out = new List<OutputLine>();
            Buffer = new OutputLine("", OutputLineType.Command);
            OpenOnWrite = true;
            PauseGameWhenOpened = true;

            //Renderer
            _pixel = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _pixel.SetData(new[] { Color.White });

            BackgroundColor = new Color(0, 0, 0, 175); //new Color(0, 0, 0, 125);
            Padding = 20;
            Margin = 20;

            _width = game.GraphicsDevice.Viewport.Width;
            _height = 300;
            _position = new Vector2(Margin, 0);

            _oneCharacterWidth = _font.MeasureString("x").X;
            _maxCharactersPerLine = (int)((Bounds.Width - Padding * 2) / _oneCharacterWidth);
            //
        }

        internal void Initialize()
        {
            Engine.Input.KeyClicked += EventInput_KeyClicked;

            _scriptParser.Initialize();
            //_luaParser = new Lua();
            //_luaParser.LoadCLRPackage();
            //_luaParser.RegisterFunction("byTag", Engine.Entities, typeof(EntityManager).GetMethod("GetEntitiesByTag"));
            //_luaParser.RegisterFunction("init", this, typeof(ScriptConsole).GetMethod("LoadBasics"));
            //_luaParser.RegisterFunction("help", this, typeof(ScriptConsole).GetMethod("ShowHelp"));
            
            //string initString = "import ('JPEngine')\n" +
            //                    "import ('JPEngine.Managers')\n" +
            //                    "import ('Microsoft.Xna.Framework')\n";

            //_luaParser.DoString(initString);
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
            if (OpenOnWrite)
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
            return _font.Characters.Contains(letter);
        }

        private void ExecuteBuffer()
        {
            if (string.IsNullOrEmpty(Buffer.Output)) 
                return;

            //var output = commandProcesser.Process(Buffer.Output).Split('\n').Where(l => l != "");
            object[] result = null;
            try
            {
                Out.Add(new OutputLine(Buffer.Output, OutputLineType.Command));
                result = _scriptParser.ParseScript(Buffer.Output) as object[];
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
            if (e.Key == ToggleKey)
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
                        //Console.WriteLine(Buffer.Output);
                    }
                    break;
            }

            //switch (e.KeyCode)
            //{

            //}
        }


        #region Rendering Methods

        //TODO: Refactor out of here
        
        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_isActive) return;

            spriteBatch.Begin();
            spriteBatch.Draw(_pixel, Bounds, BackgroundColor);

            Vector2 nextCommandPosition = DrawCommands(spriteBatch, Out, FirstCommandPosition);
            nextCommandPosition = DrawPrompt(spriteBatch, nextCommandPosition);
            var bufferPosition = DrawCommand(spriteBatch, Buffer.Output, nextCommandPosition, _bufferColor); //Draw the buffer
            DrawCursor(spriteBatch, bufferPosition, gameTime);

            spriteBatch.End();
        }

        private Vector2 DrawPrompt(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.DrawString(_font, _prompt, pos, _promptColor);
            pos.X += _oneCharacterWidth*_prompt.Length + _oneCharacterWidth;
            return pos;
        }

        private Vector2 DrawCommand(SpriteBatch spriteBatch, string command, Vector2 pos, Color color)
        {
            var splitLines = command.Length > _maxCharactersPerLine ? SplitCommand(command, _maxCharactersPerLine) : new[] { command };
            foreach (var line in splitLines)
            {
                if (IsInBounds(pos.Y))
                {
                    spriteBatch.DrawString(_font, line, pos, color);
                }
                ValidateFirstCommandPosition(pos.Y + _font.LineSpacing);
                pos.Y += _font.LineSpacing;
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
                pos.Y = DrawCommand(spriteBatch, command.ToString(), pos, command.Type == OutputLineType.Command ? _pastCommandColor : _pastCommandOutputColor).Y;
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
            pos.X += _font.MeasureString(split).X;
            pos.Y -= _font.LineSpacing;
            spriteBatch.DrawString(_font,
                (int) (gameTime.TotalGameTime.TotalSeconds/_cursorBlinkSpeed)%2 == 0
                    ? _cursor
                    : "", pos, Color.OrangeRed);
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
                _firstCommandPositionOffset.Y -= _font.LineSpacing;
            }
        }

        bool IsInBounds(float yPosition)
        {
            return yPosition < _height;
        }



        #endregion


       
        
    }
}
