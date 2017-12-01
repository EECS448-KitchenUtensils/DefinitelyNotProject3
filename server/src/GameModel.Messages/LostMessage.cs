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

        /// <summary>
        /// The <see cref="Reason"/> a player has lost/left the game.
        /// </summary>
        public readonly Reason reason;

        /// <summary>
        /// Which <see cref="Player"/> has lost/left the game
        /// </summary>
        public readonly Player player;

        /// <summary>
        /// Reasons why a player could lose
        /// </summary>
        public enum Reason
        {
            /// <summary>
            /// The <see cref="Player"/> has forfeited the game
            /// </summary>
            Forfeit,
            /// <summary>
            /// The <see cref="Player"/> has failed to defend against a check
            /// </summary>
            Checkmate,
            /// <summary>
            /// The <see cref="Player"/> has had their <see cref="King"/> captured by another <see cref="Player"/>
            /// </summary>
            KingCapture
        }
    }
}
