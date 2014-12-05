using System;
using JPEngine.Events;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public interface IKeyboardHelper
    {
        void Update();

        bool IsClicked(Keys key);

        bool IsReleased(Keys key);

        bool IsDown(Keys key);

        bool IsUp(Keys key);

        Keys[] GetPressedKeys();

        event EventHandler<KeyEventArgs> KeyDown;
        event EventHandler<KeyEventArgs> KeyClicked;
        event EventHandler<KeyEventArgs> KeyReleased;
    }
}
