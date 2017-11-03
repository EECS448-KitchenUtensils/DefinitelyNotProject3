using System;
using System.Collections.Generic;
namespace GameModel
{
    class Rook : ChessPiece
    {
        public Rook(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
        }

        /// <summary>
        /// The maximum number of steps a rook can take in a single turn.
        /// </summary>
        protected override int _maxSteps => 18;

        /// <summary>
        /// The possible directions a rook can move
        /// </summary>
        protected override (int x, int y)[] _moveOffsets => _rookMoveOffsets;

        private (int x, int y)[] _rookMoveOffsets =
        {
            (1, 0),
            (0, -1),
            (-1, 0),
            (0, 1)
        };
    }
}
