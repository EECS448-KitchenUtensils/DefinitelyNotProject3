using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// A Bishop piece instance
    /// </summary>
    public class Bishop : ChessPiece
    {
        internal Bishop(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            _bishopMoves = new[]
            {
                new PositionDelta(1, 1),
                new PositionDelta(1, -1),
                new PositionDelta(-1, -1),
                new PositionDelta(-1, 1)
            };
        }

        /// <summary>
        /// The maximum number of times the move rules can be applied.
        /// Chosen based on board dimensions + extra
        /// </summary>
        protected override int _maxSteps => 18;

        /// <summary>
        /// The move rules for this piece
        /// </summary>
        protected override PositionDelta[] _moveOffsets => _bishopMoves;

        private PositionDelta[] _bishopMoves;
    }
}
