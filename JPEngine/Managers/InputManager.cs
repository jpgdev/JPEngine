using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers
{
    //TODO: Make a IInputManager to enable multiple implementations of this class
    //TODO: MonoInputManager = this version, WindowsInputManager = listen to window messages to get the correct input

    //TODO: Check the Microsoft.Xna.Framework.GameWindow.TextInput event?

    public class InputManager : Manager
    {
        #region Attributes

        private KeyboardState _newKBState;
        private KeyboardState _oldKBState;

        private MouseState _oldMouseState;
        private MouseState _newMouseState;

        public event EventHandler<KeyEventArgs> KeyDown;
        public event EventHandler<KeyEventArgs> KeyClicked;
        public event EventHandler<KeyEventArgs> KeyReleased;

        #endregion

        #region Properties

        public KeyboardState CurrentKeyboardState
        {
            get { return _newKBState; }
        }

        public MouseState CurrentMouseState
        {
            get { return _newMouseState; }
        }

        public Vector2 MousePosition
        {
            get { return new Vector2(_newMouseState.X, _newMouseState.Y); }
        }

        #endregion

        internal InputManager()
        {
        }

        internal override void Update(GameTime gameTime)
        {
            _oldKBState = _newKBState;
            _newKBState = Keyboard.GetState();

            _oldMouseState = _newMouseState;
            _newMouseState = Mouse.GetState();

            UpdateKeys();
        }
        
        private void UpdateKeys()
        {
            KeyEventArgs e = new KeyEventArgs();

            //e.Caps = (((ushort)NativeMethods.GetKeyState(0x14)) & 0xffff) != 0;

            //List<Keys> diffs = _oldKBState.GetPressedKeys().Except(_newKBState.GetPressedKeys()).ToList();

            IEnumerable<Keys> lastAndCurrentFrameKeys = _oldKBState.GetPressedKeys().Union(_newKBState.GetPressedKeys());
            //foreach (Keys key in CurrentKeyboardState.GetPressedKeys())
            foreach (Keys key in lastAndCurrentFrameKeys)
            {
                if (key == Keys.LeftAlt || key == Keys.RightAlt)
                {
                    e.Alt = true;
                }
                else if (key == Keys.LeftShift || key == Keys.RightShift)
                {
                    e.Shift = true;
                }
                else if (key == Keys.LeftControl || key == Keys.RightControl)
                {
                    e.Control = true;
                }
                else
                {
                    e.Key = key;

                    if (IsKeyDown(key))
                        OnKeyDown(this, e);

                    if (IsKeyClicked(key))
                        OnKeyClicked(this, e);

                    if(IsKeyReleased(key))
                        OnKeyReleased(this, e);
                }
            }
        }

        #region State getter

        public bool IsKeyClicked(Keys key)
        {
            return (_newKBState.IsKeyDown(key) && _oldKBState.IsKeyUp(key));
        }

        public bool IsKeyReleased(Keys key)
        {
            return (_newKBState.IsKeyUp(key) && _oldKBState.IsKeyDown(key));
        }

        public bool IsKeyDown(Keys key)
        {
            return _newKBState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _newKBState.IsKeyUp(key);
        }

        public bool LeftMouseButtonClicked()
        {
            return (_newMouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released);
        }

        public bool LeftMouseButtonDown()
        {
            return (_newMouseState.LeftButton == ButtonState.Pressed);
        }

        public bool RightMouseButtonClicked()
        {
            return (_newMouseState.RightButton == ButtonState.Pressed &&
                    _oldMouseState.RightButton == ButtonState.Released);
        }

        public bool RightMouseButtonDown()
        {
            return (_newMouseState.RightButton == ButtonState.Pressed);
        }

        #endregion

        #region Event handlers

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(sender, e);
        }

        private void OnKeyClicked(object sender, KeyEventArgs e)
        {
            if (KeyClicked != null)
                KeyClicked(sender, e);
        }


        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            if (KeyReleased != null)
                KeyReleased(sender, e);
        }

        #endregion

        public static char GetCharValue(Keys key, bool shiftClicked)
        {
            //bool shift = IsKeyDown(Keys.LeftShift) || IsKeyDown(Keys.LeftShift);
            
            switch (key)
            {
                //Alphabet keys
                case Keys.A: return shiftClicked ? 'A' : 'a'; 
                case Keys.B: return shiftClicked ? 'B' : 'b'; 
                case Keys.C: return shiftClicked ? 'C' : 'c'; 
                case Keys.D: return shiftClicked ? 'D' : 'd'; 
                case Keys.E: return shiftClicked ? 'E' : 'e'; 
                case Keys.F: return shiftClicked ? 'F' : 'f'; 
                case Keys.G: return shiftClicked ? 'G' : 'g'; 
                case Keys.H: return shiftClicked ? 'H' : 'h'; 
                case Keys.I: return shiftClicked ? 'I' : 'i'; 
                case Keys.J: return shiftClicked ? 'J' : 'j'; 
                case Keys.K: return shiftClicked ? 'K' : 'k'; 
                case Keys.L: return shiftClicked ? 'L' : 'l'; 
                case Keys.M: return shiftClicked ? 'M' : 'm'; 
                case Keys.N: return shiftClicked ? 'N' : 'n'; 
                case Keys.O: return shiftClicked ? 'O' : 'o'; 
                case Keys.P: return shiftClicked ? 'P' : 'p'; 
                case Keys.Q: return shiftClicked ? 'Q' : 'q'; 
                case Keys.R: return shiftClicked ? 'R' : 'r'; 
                case Keys.S: return shiftClicked ? 'S' : 's'; 
                case Keys.T: return shiftClicked ? 'T' : 't'; 
                case Keys.U: return shiftClicked ? 'U' : 'u'; 
                case Keys.V: return shiftClicked ? 'V' : 'v'; 
                case Keys.W: return shiftClicked ? 'W' : 'w'; 
                case Keys.X: return shiftClicked ? 'X' : 'x'; 
                case Keys.Y: return shiftClicked ? 'Y' : 'y'; 
                case Keys.Z: return shiftClicked ? 'Z' : 'z'; 

                //Decimal keys
                case Keys.D0: return shiftClicked ? ')' : '0'; 
                case Keys.D1: return shiftClicked ? '!' : '1'; 
                case Keys.D2: return shiftClicked ? '@' : '2'; 
                case Keys.D3: return shiftClicked ? '#' : '3'; 
                case Keys.D4: return shiftClicked ? '$' : '4'; 
                case Keys.D5: return shiftClicked ? '%' : '5'; 
                case Keys.D6: return shiftClicked ? '^' : '6'; 
                case Keys.D7: return shiftClicked ? '&' : '7'; 
                case Keys.D8: return shiftClicked ? '*' : '8'; 
                case Keys.D9: return shiftClicked ? '(' : '9'; 

                //Decimal numpad keys
                case Keys.NumPad0: return '0'; 
                case Keys.NumPad1: return '1'; 
                case Keys.NumPad2: return '2'; 
                case Keys.NumPad3: return '3'; 
                case Keys.NumPad4: return '4'; 
                case Keys.NumPad5: return '5'; 
                case Keys.NumPad6: return '6'; 
                case Keys.NumPad7: return '7'; 
                case Keys.NumPad8: return '8'; 
                case Keys.NumPad9: return '9'; 

                //Special keys
                case Keys.OemTilde: return shiftClicked ? '~' : '`'; 
                case Keys.OemSemicolon: return shiftClicked ? ':' : ';'; 
                case Keys.OemQuotes: return shiftClicked ? '"' : '\''; 
                case Keys.OemQuestion: return shiftClicked ? '?' : '/'; 
                case Keys.OemPlus: return shiftClicked ? '+' : '='; 
                case Keys.OemPipe: return shiftClicked ? '|' : '\\'; 
                case Keys.OemPeriod: return shiftClicked ? '>' : '.'; 
                case Keys.OemOpenBrackets: return shiftClicked ? '{' : '['; 
                case Keys.OemCloseBrackets: return shiftClicked ? '}' : ']'; 
                case Keys.OemMinus: return shiftClicked ? '_' : '-'; 
                case Keys.OemComma: return shiftClicked ? '<' : ','; 
                case Keys.Space: return ' '; 
            }

            return (char)0;
        }

    }
}