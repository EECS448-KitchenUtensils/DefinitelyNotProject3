using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// Represents an individual player
    /// </summary>
    public class ChessPlayer {
        public ChessPlayer(PlayerEnum me)
        {
            Player = me;
        }
        /// <summary>
        /// Which player slot this is (1..4)
        /// </summary>
        /// <returns>A copy of this PlayerEnum</returns>
        public PlayerEnum Player {get; private set;}
    }
}