using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Queen : ChessPiece
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
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