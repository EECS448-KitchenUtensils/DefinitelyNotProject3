using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Queen : ChessPiece
    {
        public override IEnumerable<IValidMoveResult> PossibleMoves {
            get {
                return Enumerable.Empty<IValidMoveResult>();
            }
        }
        public override bool CanMoveTo(BoardPosition position)
        {
            throw new System.NotImplementedException();
        }
    }
}