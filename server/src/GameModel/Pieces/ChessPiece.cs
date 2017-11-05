using GameModel.Data;
using System;
using System.Collections.Generic;

namespace GameModel
{
    public abstract class ChessPiece
    {
        /// <summary>
        /// Enumerates all of the valid possible moves for this piece
        /// </summary>
        /// <param name="from">The position to move from</param>
        /// <param name="positionChecker">A function that checks if a piece is at a given position and also bounds checks</param>
        /// <returns>The valid moves for this piece</returns>
        public virtual IEnumerable<MoveResult> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker)
        {
            for (var i = 0; i < _moveOffsets.Length; i++)
            {
                for (int step = 1; step <= _maxSteps; step++)
                {
                    var newX = Position.X + (_moveOffsets[i].X * step);
                    var newY = Position.Y + (_moveOffsets[i].Y * step);
                    var candidate = new BoardPosition(newX, newY);
                    //Empty space => valid move and keep going
                    //Enemy => Capture is valid, but cannot jump piece
                    //Friendly => Move not valid
                    var status = positionChecker(candidate);
                    if (status == SpaceStatus.Empty)
                    {
                        yield return new MoveResult(candidate, MoveType.Move);
                    }
                    else if (status == SpaceStatus.Enemy)
                    {
                        yield return new MoveResult(candidate, MoveType.Capture);
                        break;
                    }
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// The owner of this piece
        /// </summary>
        /// <returns>A ChessPlayer reference</returns>
        public PlayerEnum Owner { get; protected set; }

        /// <summary>
        /// The current position of this piece on the board
        /// </summary>
        public virtual BoardPosition Position { get; internal set; }

        /// <summary>
        /// This should be overridden with the directions that a piece can move
        /// </summary>
        protected virtual PositionDelta[] _moveOffsets { get; }

        /// <summary>
        /// This should be overriden with the maximum number of times that _moveOffsets 
        /// can be applied.
        /// </summary>
        protected virtual int _maxSteps { get; }
    }
}