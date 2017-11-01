using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Bishop : ChessPiece
    {
        public Bishop()
        {
        }
        /// <summary>
        /// Enumerates all possible moves from a bishop
        /// </summary>
        /// <param name="pos">The position to start from</param>
        /// <returns>A stream of potential positions</returns>
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos)
        {
            var directions = new[,] {{-1, -1}, {-1, 1}, {1, -1}, {-1, -1}};
            for(var i = 0; i < directions.Length; i++) {
                for(int centerDistance = 1; true; centerDistance++)
                {
                    var newX = pos.x + (directions[i, 0] * centerDistance);
                    var newY = pos.y + (directions[i, 1] * centerDistance);
                    var candidate = new BoardPosition(newX, newY);
                    if (ChessBoard.CheckPositionExists(candidate))
                        yield return candidate;
                    else
                        break;
                }
            }
        }

        public override bool CanMoveTo(BoardPosition position)
        {
            throw new System.NotImplementedException();
        }
    }
}
