using System;
using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public class GamePadHelper : IGamePadHelper
    {
        public IGamePadInfos GamePad1 { get; private set; }
        public IGamePadInfos GamePad2 { get; private set; }
        public IGamePadInfos GamePad3 { get; private set; }
        public IGamePadInfos GamePad4 { get; private set; }

        public GamePadHelper()
        {
            GamePad1 = new GamePadInfos(PlayerIndex.One);
            GamePad2 = new GamePadInfos(PlayerIndex.Two);
            GamePad3 = new GamePadInfos(PlayerIndex.Three);
            GamePad4 = new GamePadInfos(PlayerIndex.Four);
        }

        public GamePadHelper(IGamePadInfos gamePad1, IGamePadInfos gamePad2, IGamePadInfos gamePad3, IGamePadInfos gamePad4)
        {
            if (gamePad1 == null)
                throw new ArgumentNullException("gamePad1");

            if (gamePad2 == null)
                throw new ArgumentNullException("gamePad2");

            if (gamePad3 == null)
                throw new ArgumentNullException("gamePad3");

            if (gamePad4 == null)
                throw new ArgumentNullException("gamePad4");

            GamePad1 = gamePad1;
            GamePad2 = gamePad2;
            GamePad3 = gamePad3;
            GamePad4 = gamePad4;
        }

        public void Update()
        {
            GamePad1.Update();
            GamePad2.Update();
            GamePad3.Update();
            GamePad4.Update();
        }
    }
}
