using System;

namespace GameModel.Data
{
    /// <summary>
    /// Holds state that belongs to a user playing the game
    /// </summary>
    public class Player : IEquatable<Player>
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
        /// Disables this <see cref="Player"/> because of forfeit
        /// </summary>
        public void Forfeit()
        {
            InGame = false;
        }

        /// <summary>
        /// Disables this <see cref="Player"/> because of loss (King capture, checkmate, etc)
        /// </summary>
        public void Loss()
        {
            InGame = false;
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

        /// <summary>
        /// Checks structural equality
        /// </summary>
        /// <param name="obj">Another <see cref="object"/> to check against</param>
        /// <returns>true if structural equal, else false</returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Player other:
                    return Equals(other);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks structural equality
        /// </summary>
        /// <param name="other">Another <see cref="Player"/> to check against</param>
        /// <returns>true if structural equal, else false</returns>
        public bool Equals(Player other)
        {
            if (other == null)
                return false;
            return Precedence == other.Precedence &&
                   Checked == other.Checked &&
                   InGame == other.InGame;
        }

        /// <summary>
        /// Generates a unchanging hash code
        /// </summary>
        /// <returns>A hash code</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
