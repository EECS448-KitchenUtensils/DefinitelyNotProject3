using GameModel.Data;

namespace GameModel
{
    class Bishop : ChessPiece
    {
        public Bishop(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
        }

        protected override int _maxSteps => 18; //Chosen based on board dimensions + extra
        protected override (int x, int y)[] _moveOffsets => _bishopMoves;

        private (int x, int y)[] _bishopMoves =
        {
            (1, 1),
            (1, -1),
            (-1, -1),
            (-1, -1)
        };
    }
}
