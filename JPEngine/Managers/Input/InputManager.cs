using System;
using JPEngine.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public class InputManager : Manager, IInputManager
    {
        #region Properties

        public IKeyboardHelper Keyboard { get; private set; }

        public IMouseHelper Mouse { get; private set; }

        public IGamePadHelper GamePads { get; private set; }
        
        #endregion

        internal InputManager()
        {
            Keyboard = new KeyboardHelper();
            Mouse = new MouseHelper();
            GamePads = new GamePadHelper();
        }

        internal InputManager(IKeyboardHelper keyboardHelper, IMouseHelper mouseHelper, IGamePadHelper gamePadHelper)
        {
            if(keyboardHelper == null)
                throw new ArgumentNullException("keyboardHelper");

            if (mouseHelper == null)
                throw new ArgumentNullException("mouseHelper");

            if (gamePadHelper == null)
                throw new ArgumentNullException("gamePadHelper");

            Keyboard = keyboardHelper;
            Mouse = mouseHelper;
            GamePads = gamePadHelper;
        }

        #region IsClicked

        public bool IsClicked(Keys key)
        {
            return Keyboard.IsClicked(key);
        }

        public bool IsClicked(MouseButton button)
        {
            return Mouse.IsClicked(button);
        }

        public bool IsClicked(Buttons button, int playerIndex = 0)
        {
            return GamePads[playerIndex].IsClicked(button);
        }

        #endregion


        #region IsReleased

        public bool IsReleased(Keys key)
        {
            return Keyboard.IsReleased(key);
        }

        public bool IsReleased(MouseButton button)
        {
            return Mouse.IsReleased(button);
        }

        public bool IsReleased(Buttons button, int playerIndex = 0)
        {
            return GamePads[playerIndex].IsReleased(button);
        }

        #endregion


        #region IsDown

        public bool IsDown(Keys key)
        {
            return Keyboard.IsDown(key);
        }

        public bool IsDown(MouseButton button)
        {
            return Mouse.IsDown(button);
        }

        public bool IsDown(Buttons button, int playerIndex = 0)
        {
            return GamePads[playerIndex].IsDown(button);

        }

        #endregion


        #region IsUp

        public bool IsUp(Keys key)
        {
            return Keyboard.IsUp(key);
        }

        public bool IsUp(MouseButton button)
        {
            return Mouse.IsUp(button);
        }

        public bool IsUp(Buttons button, int playerIndex = 0)
        {
            return GamePads[playerIndex].IsUp(button);
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            Keyboard.Update();
            Mouse.Update();
            GamePads.Update();
        }
    }
}