using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Knight : ChessPiece
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos, Func<BoardPosition, bool> positionChecker) =>
            _moveOffsets.Select(posMove => new BoardPosition(pos.x + posMove.x, pos.y + posMove.y))
                        .Where(candidate => ChessBoard.CheckPositionExists(candidate));

        private (int x, int y)[] _moveOffsets = 
        {
            (1, 2),
            (2, 1),
            (2, -1),
            (1, -2),
            (-1, -2),
            (-2, -1),
            (-2, 1),
            (-1, 2)
        };
    }
}