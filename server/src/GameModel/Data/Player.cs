namespace GameModel.Data
{
    public class Player
    {
        /// <summary>
        /// Creates a <see cref="Player"/> instance
        /// </summary>
        /// <param name="order">The order (1st, 2nd, etc.) that they player will take</param>
        public Player(PlayerEnum order)
        {
            Precedence = order;
            Checked = false;
            InGame = true;
        }
        
        /// <summary>
        /// The player's order (1, 2, etc.)
        /// </summary>
        public PlayerEnum Precedence { get; }

        /// <summary>
        /// Is this player in check?
        /// </summary>
        public bool Checked { get; internal set; }

        /// <summary>
        /// Is this player still in the game?
        /// </summary>
        public bool InGame { get; internal set; }
    }
}
