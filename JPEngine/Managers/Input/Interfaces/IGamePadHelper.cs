using Microsoft.Xna.Framework;

namespace JPEngine.Managers.Input
{
    public interface IGamePadHelper
    {

        /// <summary>
        /// The amount of game pads.
        /// </summary>
        int Amount { get; }

        /// <summary>
        /// Retrive a gamepad by the player index.
        /// </summary>
        /// <param name="playerIndex">he index of the player (starts at 0);</param>
        /// <returns>The corresponding GamePad.</returns>
        IGamePadInfos this[int playerIndex] { get; }

        void Update();
    }
}
