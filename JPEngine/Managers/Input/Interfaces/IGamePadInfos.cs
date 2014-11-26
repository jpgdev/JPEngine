using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public interface IGamePadInfos
    {
        bool IsConnected { get; }

        bool IsButtonUp(Buttons button);

        bool IsButtonDown(Buttons button);

        bool IsButtonClicked(Buttons button);

        bool IsButtonReleased(Buttons button);

        bool SetVibration(float leftMotor, float rightMotor);

        void Update();
    }
}
