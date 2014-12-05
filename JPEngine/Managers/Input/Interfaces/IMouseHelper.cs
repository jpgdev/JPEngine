using JPEngine.Enums;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public interface IMouseHelper
    {
        Point MousePosition { get; }

        int ScrollWheelValue { get; }

        void Update();

        bool IsClicked(MouseButton button);

        bool IsReleased(MouseButton button);

        bool IsDown(MouseButton button);

        bool IsUp(MouseButton key);
    }
}
