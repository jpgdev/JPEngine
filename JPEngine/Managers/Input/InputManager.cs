using System;
using Microsoft.Xna.Framework;

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

        public void Update()
        {
            Keyboard.Update();
            Mouse.Update();
            GamePads.Update();
        }
    }
}