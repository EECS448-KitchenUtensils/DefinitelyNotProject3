using GameModel.Data;
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
        public abstract IEnumerable<BoardPosition> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker);

        /// <summary>
        /// The owner of this piece
        /// </summary>
        /// <returns>A ChessPlayer reference</returns>
        public ChessPlayer Owner { get; protected set; }

        /// <summary>
        /// The current position of this piece on the board
        /// </summary>
        public BoardPosition Position { get; protected set; }
    }
}