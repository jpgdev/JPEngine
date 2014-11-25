using JPEngine.Enums;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public interface IMouseHelper
    {
        Point MousePosition { get; }

        int ScrollWheelValue { get; }

        void Update();

        bool IsButtonClicked(MouseButton button);

        bool IsButtonReleased(MouseButton button);

        bool IsButtonDown(MouseButton button);
    }
}
