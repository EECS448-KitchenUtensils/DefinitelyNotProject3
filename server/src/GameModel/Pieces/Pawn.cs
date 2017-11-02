using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Pawn : ChessPiece
    {
        /// <summary>
        /// Creates Pawn instance
        /// </summary>
        public Pawn()
        {
            _hasMovedYet = false;
        }
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public override IEnumerable<BoardPosition> PossibleMoves(BoardPosition pos, Func<BoardPosition, bool> positionChecker)
        {
            //Check both capture possiblities
            foreach(var offset in _captureOffsets)
            {
                var captureCandidate = new BoardPosition(pos.x + offset.x, pos.y + offset.y);
                if (ChessBoard.CheckPositionExists(captureCandidate) && positionChecker(captureCandidate))
                    yield return captureCandidate;
            }
            var moveOffset = (_hasMovedYet) ? _hasMovedMoveOffset : _hasNotMovedMoveOffset;
            var moveCandidate = new BoardPosition(pos.x + moveOffset.x, pos.y + moveOffset.y);
            if (ChessBoard.CheckPositionExists(moveCandidate))
                yield return moveCandidate;
        }
        private bool _hasMovedYet;
        private (int x, int y)[] _captureOffsets =
        {
            (-1, 1),
            (1, 1)
        };
        private (int x, int y) _hasMovedMoveOffset = (0, 1);
        private (int x, int y) _hasNotMovedMoveOffset = (0, 2);
    }
}