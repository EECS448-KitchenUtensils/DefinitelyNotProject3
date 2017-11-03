using GameModel.Data;
using System;
using System.Collections.Generic;

namespace GameModel
{
    /// <summary>
    /// Represents a Pawn piece. Easily the weirdest piece to implement.
    /// </summary>
    class Pawn : ChessPiece
    {
        /// <summary>
        /// Creates a Pawn instance
        /// </summary>
        /// <param name="owner">The owner of this Pawn</param>
        /// <param name="initialPosition">The initial position on the board</param>
        public Pawn(PlayerEnum owner, BoardPosition initialPosition)
        {
            _hasMovedYet = false;
            Owner = owner;
            Position = initialPosition;
        }
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public override IEnumerable<BoardPosition> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker)
        {
            //Check both capture possiblities
            foreach(var offset in _captureOffsets)
            {
                var captureCandidate = new BoardPosition(Position.x + offset.x, Position.y + offset.y);
                if (ChessBoard.CheckPositionExists(captureCandidate)
                    && positionChecker(captureCandidate) == SpaceStatus.Enemy)
                    yield return captureCandidate;
            }
            var moveOffset = (_hasMovedYet) ? _hasMovedMoveOffset : _hasNotMovedMoveOffset;
            var moveCandidate = new BoardPosition(Position.x + moveOffset.x, Position.y + moveOffset.y);
            if (ChessBoard.CheckPositionExists(moveCandidate))
                yield return moveCandidate;
        }

        /// <summary>
        /// Overrides the base Position property to set _hasMovedYet to true on updating
        /// </summary>
        public override BoardPosition Position
        {
            get => base.Position;
            protected set
            {
                _hasMovedYet = true;
                base.Position = value;
            }
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