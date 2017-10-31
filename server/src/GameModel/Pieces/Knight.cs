using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Knight : ChessPiece
    {
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos) =>
            _moveOffsets.Select(posMove => new BoardPosition(pos.x + posMove.x, pos.y + posMove.y))
                        .Where(candidate => ChessBoard.CheckPositionExists(candidate));
        public override bool CanMoveTo(BoardPosition position)
        {
            throw new System.NotImplementedException();
        }
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