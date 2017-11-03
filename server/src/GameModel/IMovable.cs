using System;
using System.Collections.Generic;
using GameModel.Data;

namespace GameModel
{
    public interface IMoveable {
        /// <summary>
        /// All of this piece's possible moves
        /// </summary>
        /// <param name="from">The position to start from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>Valid moves that can be taken</returns>
        IEnumerable<BoardPosition> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker);
    }
}