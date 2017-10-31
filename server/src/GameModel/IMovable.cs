using System.Collections.Generic;

namespace GameModel
{
    public interface IMoveable {
        /// <summary>
        /// All of this piece's possible moves
        /// </summary>
        /// <returns>Valid moves that can be taken</returns>
        IEnumerable<PositionDelta> PossibleMoves {get;}

        /// <summary>
        /// Checks whether this piece can move to the given position
        /// </summary>
        /// <param name="dest">The position to check for</param>
        /// <returns>true if can move, false otherwise</returns>
        bool CanMoveTo (BoardPosition dest);
    }
}