using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    /// <summary>
    /// Represents a Pawn piece. Easily the weirdest piece to implement.
    /// </summary>
    public class Pawn : ChessPiece
    {
        /// <summary>
        /// Creates a Pawn instance
        /// </summary>
        /// <param name="owner">The owner of this Pawn</param>
        /// <param name="initialPosition">The initial position on the board</param>
        internal Pawn(PlayerEnum owner, BoardPosition initialPosition)
        {
            _hasMovedYet = false;
            Owner = owner;
            _position = initialPosition;
            _hasMovedMoveOffset = new[]
            {
                //Player 1
                new PositionDelta(0, 1),
                //Player 2
                new PositionDelta(-1, 0),
                //Player 3
                new PositionDelta(0, -1),
                //Player 4
                new PositionDelta(1, 0)
            };
            _hasNotMovedMoveOffset = new[]
            {
                //Player 1
                new PositionDelta(0, 2),
                //Player 2
                new PositionDelta(-2, 0),
                //Player 3
                new PositionDelta(0, -2),
                //Player 4
                new PositionDelta(2, 0)
            };
            _leftCaptureOffsets = new[]
            {
                //Player 1
                new PositionDelta(-1, 1),
                //Player 2
                new PositionDelta(-1, -1),
                //Player 3
                new PositionDelta(-1, -1),
                //Player 4
                new PositionDelta(1, -1)
            };
            _rightCaptureOffsets = new[]
            {
                //Player 1
                new PositionDelta(1, 1),
                //Player 2
                new PositionDelta(-1, 1),
                //Player 3
                new PositionDelta(1, -1),
                //Player 4
                new PositionDelta(1, 1)
            };
        }
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position.</param>
        /// <note><cref>positionChecker</cref must do bounds checking</note>
        /// <returns>The valid moves for this piece</returns>
        public override IEnumerable<MoveResult> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker)
        {
            //Check both capture possiblities
            var captureOffsets = new[] { _leftCaptureOffsets[(int)Owner], _rightCaptureOffsets[(int)Owner] };
            var laterMoveOffset = _hasMovedMoveOffset[(int)Owner];
            var firstMoveOffset = _hasNotMovedMoveOffset[(int)Owner];
            //At the beginning of the game, the pawn can move either one or two spaces forward
            var moveOffsets = (_hasMovedYet) ? new[] { laterMoveOffset } : new[] { firstMoveOffset, laterMoveOffset };
            var capturePositions = captureOffsets.Select(offset => Position + offset)
                                                 .Where(position => positionChecker(position) == SpaceStatus.Enemy)
                                                 .Select(position => new MoveResult(position, MoveType.Capture));
            var forwardPositions = moveOffsets.Select(offset => Position + offset)
                                              .Where(position => positionChecker(position) == SpaceStatus.Empty)
                                              .Select(position => new MoveResult(position, MoveType.Move));
            return capturePositions.Concat(forwardPositions);
        }

        /// <summary>
        /// Overrides the base Position property to set _hasMovedYet to true on updating
        /// </summary>
        public override BoardPosition Position
        {
            get => _position;
            set
            {
                _hasMovedYet = true;
                _position = value;
            }
        }

        private bool _hasMovedYet;
        private PositionDelta[] _leftCaptureOffsets;
        private PositionDelta[] _rightCaptureOffsets;
        private BoardPosition _position;
        private PositionDelta[] _hasMovedMoveOffset;
        private PositionDelta[] _hasNotMovedMoveOffset;
    }
}