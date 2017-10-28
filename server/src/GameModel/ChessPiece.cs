using System.Collections.Generic;

namespace GameModel
{
    public abstract class ChessPiece : IMoveable
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <returns>The valid moves for this piece</returns>
        public abstract IEnumerable<IValidMoveResult> PossibleMoves { get; }
        
        /// <summary>
        /// The owner of this piece
        /// </summary>
        /// <returns>A ChessPlayer reference</returns>
        public abstract ChessPlayer Owner { get; protected set; }

        /// <summary>
        /// Checks if this piece can move to a given location
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns>true if the move is valid</returns>
        public abstract bool CanMoveTo(BoardPosition position);
    }
}