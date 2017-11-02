using System;
using System.Collections.Generic;
namespace GameModel
{
    class Rook : ChessPiece
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos, Func<BoardPosition, bool> positionChecker)
        {
            for(int i = 0; i < 4; i++) {

                // iterate until no board position exists
                for(int centerDistance = 1; true; centerDistance++)
                {
                    var newX = pos.x + (_moveOffsets[i].x * centerDistance);
                    var newY = pos.y + (_moveOffsets[i].y * centerDistance);
                    var candidate = new BoardPosition(newX, newY);

                    if (ChessBoard.CheckPositionExists(candidate))
                    {
                        yield return candidate;
                        if (positionChecker(candidate))
                            break;
                    }
                    else
                        break;
                }
            }
        }

        private (int x, int y)[] _moveOffsets =
        {
            (1, 0),
            (0, -1),
            (-1, 0),
            (0, 1)
        };
    }
}
