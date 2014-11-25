using System;
using JPEngine.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public interface IKeyboardHelper
    {
        void Update();

        bool IsKeyClicked(Keys key);

        bool IsKeyReleased(Keys key);

        bool IsKeyDown(Keys key);

        bool IsKeyUp(Keys key);

        Keys[] GetPressedKeys();

        event EventHandler<KeyEventArgs> KeyDown;
        event EventHandler<KeyEventArgs> KeyClicked;
        event EventHandler<KeyEventArgs> KeyReleased;
    }
}
