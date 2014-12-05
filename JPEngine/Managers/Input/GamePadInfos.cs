using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JPEngine.Managers.Input
{
    public class GamePadInfos : IGamePadInfos
    {
        private GamePadState _oldGamePadState;
        private GamePadState _newGamePadState;

        private readonly GamePadCapabilities _gamePadCapabilities;
        private readonly PlayerIndex _playerIndex;

        //public GamePadCapabilities GamePadCapabilities
        //{
        //    get { return _gamePadCapabilities; }
        //}

        public bool IsConnected
        {
            get { return _gamePadCapabilities.IsConnected; }
        }

        public GamePadInfos(PlayerIndex playerIndex)
        {
            _playerIndex = playerIndex;
            _gamePadCapabilities = GamePad.GetCapabilities(playerIndex);
        }

        public bool IsUp(Buttons button)
        {
            return _newGamePadState.IsButtonUp(button);
        }

        public bool IsDown(Buttons button)
        {
            return _newGamePadState.IsButtonDown(button);
        }

        public bool IsClicked(Buttons button)
        {
            return _oldGamePadState.IsButtonUp(button) && _newGamePadState.IsButtonDown(button);
        }

        public bool IsReleased(Buttons button)
        {
            return _oldGamePadState.IsButtonDown(button) && _newGamePadState.IsButtonUp(button);
        }

        public bool SetVibration(float leftMotor, float rightMotor)
        {
            return GamePad.SetVibration(_playerIndex, leftMotor, rightMotor);
        }

        public void Update()
        {
            _oldGamePadState = _newGamePadState;
            _newGamePadState = GamePad.GetState(_playerIndex);
        }
    }
}
