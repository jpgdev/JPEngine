using System;
using System.Collections.Generic;
using System.Linq;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers
{
    //TODO: Make a IInputManager to enable multiple implementations of this class
    //TODO: MonoInputManager = this version, WindowsInputManager = listen to window messages to get the correct input

    //TODO: Check the Microsoft.Xna.Framework.GameWindow.TextInput event?

    //TODO: Split this in 3 parts: KeyboardHelper, MouseHelper, GamePadHelper and keep them all here, but it would be possible to change them around 

    public class GamePadInfos
    {
        private GamePadState _oldGamePadState;
        private GamePadState _newGamePadState;

        private readonly GamePadCapabilities _gamePadCapabilities;
        private readonly PlayerIndex _playerIndex;

        public GamePadCapabilities GamePadCapabilities
        {
            get { return _gamePadCapabilities; }
        }

        public bool IsConnected
        {
            get { return _gamePadCapabilities.IsConnected; }
        }

        public GamePadInfos(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
            _gamePadCapabilities = GamePad.GetCapabilities(playerIndex);
        }

        public bool IsButtonUp(Buttons button)
        {
            return _newGamePadState.IsButtonUp(button);
        }

        public bool IsButtonDown(Buttons button)
        {
            return _newGamePadState.IsButtonDown(button);
        }

        public bool IsButtonClicked(Buttons button)
        {
            return _oldGamePadState.IsButtonUp(button) && _newGamePadState.IsButtonDown(button);
        }

        public bool IsButtonReleased(Buttons button)
        {
            return _oldGamePadState.IsButtonDown(button) && _newGamePadState.IsButtonUp(button);
        }

        public bool SetVibration(float leftMotor, float rightMotor)
        {
            return GamePad.SetVibration(_playerIndex, leftMotor, rightMotor);
        }

        public void Update(GameTime gameTime)
        {
            _oldGamePadState = _newGamePadState;
            _newGamePadState = GamePad.GetState(_playerIndex);
        }
    }

    public class InputManager : Manager
    {
        #region Attributes
        
        private KeyboardState _newKBState;
        private KeyboardState _oldKBState;

        private MouseState _oldMouseState;
        private MouseState _newMouseState;

        private readonly GamePadInfos _gamePad1;
        private readonly GamePadInfos _gamePad2;
        private readonly GamePadInfos _gamePad3;
        private readonly GamePadInfos _gamePad4;

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

        public GamePadInfos GamePad1
        {
            get { return _gamePad1; }
        }

        public GamePadInfos GamePad2
        {
            get { return _gamePad2; }
        }

        public GamePadInfos GamePad3
        {
            get { return _gamePad3; }
        }

        public GamePadInfos GamePad4
        {
            get { return _gamePad4; }
        }

        #endregion

        internal InputManager()
        {
            _gamePad1 = new GamePadInfos(PlayerIndex.One);
            _gamePad2 = new GamePadInfos(PlayerIndex.Two);
            _gamePad3 = new GamePadInfos(PlayerIndex.Three);
            _gamePad4 = new GamePadInfos(PlayerIndex.Four);
        }

        internal override void Update(GameTime gameTime)
        {
            _oldKBState = _newKBState;
            _newKBState = Keyboard.GetState();

            _oldMouseState = _newMouseState;
            _newMouseState = Mouse.GetState();

            _gamePad1.Update(gameTime);
            _gamePad2.Update(gameTime);
            _gamePad3.Update(gameTime);
            _gamePad4.Update(gameTime);

            UpdateKeys();
        }
        
        private void UpdateKeys()
        {
            KeyEventArgs e = new KeyEventArgs();

            //e.Caps = (((ushort)NativeMethods.GetKeyState(0x14)) & 0xffff) != 0;

            //List<Keys> diffs = _oldKBState.GetPressedKeys().Except(_newKBState.GetPressedKeys()).ToList();

            //Get all the pressed keys from this frame and the last frame.
            IEnumerable<Keys> lastAndCurrentFrameKeys = _oldKBState.GetPressedKeys().Union(_newKBState.GetPressedKeys());
            foreach (Keys key in lastAndCurrentFrameKeys)
            {
                switch (key)
                {
                    case Keys.RightAlt:
                    case Keys.LeftAlt:
                        e.Alt = true;
                        break;

                    case Keys.RightShift:
                    case Keys.LeftShift:
                        e.Shift = true;
                        break;

                    case Keys.RightControl:
                    case Keys.LeftControl:
                        e.Control = true;
                        break;

                    default:
                        e.Key = key;

                        if (IsKeyDown(key))
                            OnKeyDown(this, e);

                        if (IsKeyClicked(key))
                            OnKeyClicked(this, e);

                        if(IsKeyReleased(key))
                            OnKeyReleased(this, e);

                        break;
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