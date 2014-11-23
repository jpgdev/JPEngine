using System;
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
        public Color SelectionColor { get; set; }
        public Color BufferColor { get; set; }
        public Color CursorColor { get; set; }
        public Color PastCommandColor { get; set; }
        public Color PastCommandOutputColor { get; set; }

        

        //TODO: ScrollUpKey, ScrollDownKey, IsScrollable, IsClickable (Mouse) (if not, the mouse will ignore it)

        public ConsoleOptions(SpriteFont font)
        {
            //TODO: Find a way to have a Font in the Engine, without relying on the Game
            if (font == null)
                throw new ArgumentNullException("font");

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
            SelectionColor = new Color(Color.DarkRed, 175);
        }
    }

    public class ScriptConsole : IDisposable
    {
        private readonly ConsoleInputProcessor _consoleInputProcessor;
        private readonly ConsoleRenderer _consoleRenderer;

        public bool IsActive { get { return _consoleInputProcessor.IsActive;}}

        public ConsoleOptions Options { get; private set; }

        public event EventHandler Open;
        public event EventHandler Close;

        public IScriptParser ScriptParser
        {
            get { return _consoleInputProcessor.ScriptParser; }
            set
            {
                if(value != null )
                    _consoleInputProcessor.ScriptParser = value;
            }
        }

        public ScriptConsole(ConsoleOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            Options = options;

            _consoleInputProcessor = new ConsoleInputProcessor(options);
            _consoleInputProcessor.Open += (sender, args) =>
            {
                if (Open != null)
                    Open(this, args);
            };
            _consoleInputProcessor.Close += (sender, args) =>
            {
                if (Close != null)
                    Close(this, args);
            };

            _consoleRenderer = new ConsoleRenderer(_consoleInputProcessor, options);
        }

        public void AddToBuffer(string text)
        {
            _consoleInputProcessor.AddToInputBuffer(text);
        }

        public void AddToOutput(string text)
        {
            _consoleInputProcessor.AddToOutput(text);
        }

        public void Toggle()
        {
            _consoleInputProcessor.ToggleConsole();
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!_consoleInputProcessor.IsActive) return;

            _consoleRenderer.Draw(spriteBatch, gameTime);
        }

        public void Dispose()
        {
            //todo: Dispose...
            _consoleRenderer.Dispose();
        }
    }
}
