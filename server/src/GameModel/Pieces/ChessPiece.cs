using System;
using System.Collections.Generic;

namespace GameModel
{
    public abstract class ChessPiece : IMoveable
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public abstract IEnumerable<BoardPosition> PossibleMoves(BoardPosition from, Func<BoardPosition, bool> positionChecker);

        /// <summary>
        /// The owner of this piece
        /// </summary>
        /// <returns>A ChessPlayer reference</returns>
        public ChessPlayer Owner { get; protected set; }
    }
}