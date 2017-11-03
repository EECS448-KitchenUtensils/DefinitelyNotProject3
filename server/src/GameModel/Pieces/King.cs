using GameModel.Data;

namespace GameModel
{
    class King : ChessPiece
    {
        public King(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            _kingMoveOffsets = new[]
            {
                new PositionDelta(1, 1),
                new PositionDelta(1, 0),
                new PositionDelta(1, -1),
                new PositionDelta(0, -1),
                new PositionDelta(-1, -1),
                new PositionDelta(-1, 0),
                new PositionDelta(-1, 1),
                new PositionDelta(0, 1)
            };
        }

        /// <summary>
        /// The King can only move once per turn
        /// </summary>
        protected override int _maxSteps => 1;

        /// <summary>
        /// Directions that a King can move
        /// </summary>
        protected override PositionDelta[] _moveOffsets => _kingMoveOffsets;

        private PositionDelta[] _kingMoveOffsets;
    }
}