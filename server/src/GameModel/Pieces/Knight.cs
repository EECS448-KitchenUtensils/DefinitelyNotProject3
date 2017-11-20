using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// A Knight piece instance
    /// </summary>
    public class Knight : ChessPiece
    {
        /// <summary>
        /// Creates a Knight instance
        /// </summary>
        /// <param name="owner">The owner of this piece</param>
        /// <param name="initialPosition">The initial position on the board</param>
        internal Knight(Player owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            PieceType = PieceEnum.KNIGHT;

            _knightMoveOffsets = new[]
            {
                new PositionDelta(1, 2),
                new PositionDelta(2, 1),
                new PositionDelta(2, -1),
                new PositionDelta(1, -2),
                new PositionDelta(-1, -2),
                new PositionDelta(-2, -1),
                new PositionDelta(-2, 1),
                new PositionDelta(-1, 2)
            };
        }

        /// <summary>
        /// A Knight can only move once per turn
        /// </summary>
        protected override int _maxSteps => 1;

        /// <summary>
        /// The possible offsets that a Knight can move to
        /// </summary>
        protected override PositionDelta[] _moveOffsets => _knightMoveOffsets;

        private PositionDelta[] _knightMoveOffsets;
    }
}