using GameModel.Data;
using System;

namespace GameModel.Messages
{
    /// <summary>
    /// Emitted when a turn is advanced
    /// </summary>
    [Serializable]
    public class SetTurnMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="SetTurnMessage"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> that will own this turn</param>
        public SetTurnMessage(Player player)
        {
            this.player = player.Precedence;
        }

        /// <summary>
        /// The player that owns the new turn
        /// </summary>
        public readonly PlayerEnum player;
    }
}
