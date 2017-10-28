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
    }
}