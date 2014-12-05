using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public class GamePadHelper : IGamePadHelper
    {
        private readonly int _amount = 4;
        private readonly IGamePadInfos[] _gamePads;

        public int Amount { get { return _amount; } }
        
        public IGamePadInfos this[int playerIndex]
        {
            get { return _gamePads[playerIndex]; }
        }

        public GamePadHelper()
        {
            _gamePads = new IGamePadInfos[]
            {
                new GamePadInfos(PlayerIndex.One),
                new GamePadInfos(PlayerIndex.Two),
                new GamePadInfos(PlayerIndex.Three),
                new GamePadInfos(PlayerIndex.Four)
            };
        }

        public GamePadHelper(params IGamePadInfos[] gamePads)
        {
            if (gamePads == null)
                throw new ArgumentNullException("gamePads");

            if(gamePads.Length < 1)
                throw new ArgumentException("The array of GamePads must have at least one entry.");

            gamePads.CopyTo(_gamePads, 0);
            _amount = _gamePads.Length;
        }

        public void Update()
        {
            foreach (IGamePadInfos gamePad in _gamePads)
                gamePad.Update();
        }

    }
}
