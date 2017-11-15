using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// A Queen piece instance
    /// </summary>
    public class Queen : ChessPiece
    {
        /// <summary>
        /// Creates a Queen instance
        /// </summary>
        /// <param name="owner">The owner of this Queen</param>
        /// <param name="initialPosition">The initial position on the board</param>
        internal Queen(Player owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
            PieceType = PieceEnum.QUEEN;

            _queenMoveOffsets = new[]
            {
                //rook moves
                new PositionDelta(1, 0),
                new PositionDelta(0, -1),
                new PositionDelta(-1, 0),
                new PositionDelta(0, 1),
                //bishop moves
                new PositionDelta(1, 1),
                new PositionDelta(1, -1),
                new PositionDelta(-1, -1),
                new PositionDelta(-1, 1)
            };
        }
        /// <summary>
        /// The maximum number of steps a Queen can take per turn. It's a guess.
        /// </summary>
        protected override int _maxSteps => 18;

        /// <summary>
        /// The possible moves a queen can make consist of the union of the rook and bishop
        /// </summary>
        protected override PositionDelta[] _moveOffsets => _queenMoveOffsets;

        private PositionDelta[] _queenMoveOffsets;
    }
}