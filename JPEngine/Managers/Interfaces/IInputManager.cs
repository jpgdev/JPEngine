using JPEngine.Managers.Input;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers
{
    public interface IInputManager : IManager
    {
        IKeyboardHelper Keyboard { get; }

        IMouseHelper Mouse { get; }

        IGamePadHelper GamePads { get; }

        void Update(GameTime gameTime);
    }
}
