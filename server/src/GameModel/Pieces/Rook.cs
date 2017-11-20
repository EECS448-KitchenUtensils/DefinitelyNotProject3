using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// A Rook piece instance
    /// </summary>
    public class Rook : ChessPiece
    {
        internal Rook(Player owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            PieceType = PieceEnum.ROOK;

            _rookMoveOffsets = new[]
            {
                new PositionDelta(1, 0),
                new PositionDelta(0, -1),
                new PositionDelta(-1, 0),
                new PositionDelta(0, 1)
            };
        }

        /// <summary>
        /// The maximum number of steps a rook can take in a single turn.
        /// </summary>
        protected override int _maxSteps => 18;

        /// <summary>
        /// The possible directions a rook can move
        /// </summary>
        protected override PositionDelta[] _moveOffsets => _rookMoveOffsets;

        private PositionDelta[] _rookMoveOffsets;
    }
}
