using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Knight : ChessPiece
    {
        /// <summary>
        /// Creates a Knight instance
        /// </summary>
        /// <param name="owner">The owner of this piece</param>
        /// <param name="initialPosition">The initial position on the board</param>
        public Knight(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
        }

        /// <summary>
        /// A Knight can only move once per turn
        /// </summary>
        protected override int _maxSteps => 1;

        /// <summary>
        /// The possible offsets that a Knight can move to
        /// </summary>
        protected override (int x, int y)[] _moveOffsets => _knightMoveOffsets;

        private (int x, int y)[] _knightMoveOffsets = 
        {
            (1, 2),
            (2, 1),
            (2, -1),
            (1, -2),
            (-1, -2),
            (-2, -1),
            (-2, 1),
            (-1, 2)
        };
    }
}