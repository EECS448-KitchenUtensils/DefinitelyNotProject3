using GameModel.Data;

namespace GameModel.Messages
{
    /// <summary>
    /// Communicates to Unity that a player has been removed from the game
    /// </summary>
    public class LostMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="LostMessage"/>
        /// </summary>
        /// <param name="reason">The reason why this player lost</param>
        /// <param name="player">Which player lost</param>
        public LostMessage(Reason reason, Player player)
        {
            this.reason = reason;
            this.player = player;
        }

        private LostMessage() { }

        public readonly Reason reason;
        public readonly Player player;

        /// <summary>
        /// Reasons why a player could lose
        /// </summary>
        public enum Reason
        {
            Forfeit,
            Checkmate,
            KingCapture
        }
    }
}
