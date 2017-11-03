using GameModel.Data;

namespace GameModel
{
    class King : ChessPiece
    {
        public King(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
        }

        /// <summary>
        /// The King can only move once per turn
        /// </summary>
        protected override int _maxSteps => 1;

        /// <summary>
        /// Directions that a King can move
        /// </summary>
        protected override (int x, int y)[] _moveOffsets => _kingMoveOffsets;

        private (int x, int y)[] _kingMoveOffsets = 
        {
            (1, 1),
            (1, 0),
            (1, -1),
            (0, -1),
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, 1)
        };
    }
}