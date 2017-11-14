using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// A King piece instance
    /// </summary>
    public class King : ChessPiece
    {
        internal King(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            Check = false;
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
        public bool Check { get; internal set; }
    }
}