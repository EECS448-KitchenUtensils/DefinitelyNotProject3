using GameModel.Data;

namespace GameModel
{
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
                new PositionDelta(-1, -1)
            };
        }

        protected override int _maxSteps => 18; //Chosen based on board dimensions + extra
        protected override PositionDelta[] _moveOffsets => _bishopMoves;

        private PositionDelta[] _bishopMoves;
    }
}
