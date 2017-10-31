using System.Collections.Generic;

namespace GameModel
{
    public abstract class ChessPiece : IMoveable
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <returns>The valid moves for this piece</returns>
        public abstract IEnumerable<BoardPosition> PossibleMoves(BoardPosition from);

        /// <summary>
        /// The owner of this piece
        /// </summary>
        /// <returns>A ChessPlayer reference</returns>
        public ChessPlayer Owner { get; protected set; }

        /// <summary>
        /// Checks if this piece can move to a given location
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>true if the move is valid</returns>
        public abstract bool CanMoveTo(BoardPosition position);
    }
}