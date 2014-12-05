using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public interface IGamePadInfos
    {
        bool IsConnected { get; }

        bool IsUp(Buttons button);

        bool IsDown(Buttons button);

        bool IsClicked(Buttons button);

        bool IsReleased(Buttons button);

        bool SetVibration(float leftMotor, float rightMotor);

        void Update();
    }
}
