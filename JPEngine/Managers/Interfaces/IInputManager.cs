using JPEngine.Enums;
using JPEngine.Managers.Input;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers
{
    public interface IInputManager : IUpdateableManager
    {
        IKeyboardHelper Keyboard { get; }

        IMouseHelper Mouse { get; }

        IGamePadHelper GamePads { get; }


        bool IsClicked(Keys key);
        bool IsClicked(MouseButton key);
        bool IsClicked(Buttons key, int playerIndex = 0);
        

        bool IsReleased(Keys key);
        bool IsReleased(MouseButton key);
        bool IsReleased(Buttons key, int playerIndex = 0);


        bool IsDown(Keys key);
        bool IsDown(MouseButton key);
        bool IsDown(Buttons key, int playerIndex = 0);


        bool IsUp(Keys key);
        bool IsUp(MouseButton key);
        bool IsUp(Buttons key, int playerIndex = 0);
    }
}
