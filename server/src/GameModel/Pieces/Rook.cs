using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Rook : ChessPiece
    {
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos)
        {
            for(int i = 0; i < 4; i++) {

                // iterate until no board position exists
                for(int centerDistance = 1; true; centerDistance++)
                {
                    var newX = pos.x + (_moveOffsets[i].x * centerDistance);
                    var newY = pos.y + (_moveOffseets[i].y * centerDistance);
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

        private (int x, int y)[] _moveOffsets =
        {
            (1, 0),
            (0, -1),
            (-1, 0),
            (0, 1)
        };
    }
}
