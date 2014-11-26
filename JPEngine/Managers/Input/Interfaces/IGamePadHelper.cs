using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public interface IGamePadHelper
    {
        void Update();

        IGamePadInfos GamePad1 { get; }

        IGamePadInfos GamePad2 { get; }

        IGamePadInfos GamePad3 { get; }

        IGamePadInfos GamePad4 { get; }
    }
}
