using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    class Queen : ChessPiece
    {
        /// <summary>
        /// Creates a Queen instance
        /// </summary>
        /// <param name="owner">The owner of this Queen</param>
        /// <param name="initialPosition">The initial position on the board</param>
        public Queen(PlayerEnum owner, BoardPosition initialPosition)
        {
            Owner = owner;
            Position = initialPosition;
        }
        /// <summary>
        /// The maximum number of steps a Queen can take per turn. It's a guess.
        /// </summary>
        protected override int _maxSteps => 18;

        /// <summary>
        /// The possible moves a queen can make consist of the union of the rook and bishop
        /// </summary>
        protected override (int x, int y)[] _moveOffsets => _queenMoveOffsets;

        private (int x, int y)[] _queenMoveOffsets =
        {
            //rook moves
            (1, 0),
            (0, -1),
            (-1, 0),
            (0, 1),
            //bishop moves
            (1, 1),
            (1, -1),
            (-1, -1),
            (-1, 1)
        };
    }
}