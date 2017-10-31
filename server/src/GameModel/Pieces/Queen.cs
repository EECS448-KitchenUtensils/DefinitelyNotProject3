using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Queen : ChessPiece
    {
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos)
        {
            return Enumerable.Empty<BoardPosition>();
        }
        public override bool CanMoveTo(BoardPosition position)
        {
            throw new System.NotImplementedException();
        }
    }
}