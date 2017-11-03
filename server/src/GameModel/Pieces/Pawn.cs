using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public override IEnumerable<(BoardPosition dest, MoveType outcome)> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker)
        {
            //Check both capture possiblities
            var captureOffsets = _captureOffsets[(int)Owner];
            var laterMoveOffset = _hasMovedMoveOffset[(int)Owner];
            var firstMoveOffset = _hasNotMovedMoveOffset[(int)Owner];
            //At the beginning of the game, the pawn can move either one or two spaces forward
            var moveOffsets = (_hasMovedYet) ? new[] { laterMoveOffset } : new[] { firstMoveOffset, laterMoveOffset };
            var leftPosition = new BoardPosition(Position.x + captureOffsets.left.x, Position.y + captureOffsets.left.y);
            var rightPosition = new BoardPosition(Position.x + captureOffsets.right.x, Position.y + captureOffsets.right.y);
            var forwardPositions = moveOffsets.Select(offset => new BoardPosition(Position.x + offset.x, Position.y + offset.y));
            foreach (var position in new[] {leftPosition, rightPosition})
            {
                if (positionChecker(position) == SpaceStatus.Enemy)
                {
                    yield return (position, MoveType.Capture);
                }
            }
            foreach (var position in forwardPositions)
            {
                if (positionChecker(position) == SpaceStatus.Empty)
                    yield return (position, MoveType.Move);
            }
        }

        /// <summary>
        /// Overrides the base Position property to set _hasMovedYet to true on updating
        /// </summary>
        public override BoardPosition Position
        {
            get => base.Position;
            set
            {
                _hasMovedYet = true;
                base.Position = value;
            }
        }

        private bool _hasMovedYet;
        private ((int x, int y) left, (int x, int y) right)[] _captureOffsets =
        {
            //Player 1
            (
                (-1, 1),
                (1, 1)
            ),
            //Player 2
            (
                (-1, -1),
                (-1, 1)
            ),
            //Player 3
            (
                (-1, -1),
                (1, -1)
            ),
            //Player 4
            (
                (1, -1),
                (1, 1)
            )
        };
        private (int x, int y)[] _hasMovedMoveOffset =
        {
            //Player 1
            (0, 1),
            //Player 2
            (-1, 0),
            //Player 3
            (0, -1),
            //Player 4
            (1, 0)
        };
        private (int x, int y)[] _hasNotMovedMoveOffset =
        {
            //Player 1
            (0, 2),
            //Player 2
            (-2, 0),
            //Player 3
            (0, -2),
            //Player 4
            (2, 0)
        };
    }
}