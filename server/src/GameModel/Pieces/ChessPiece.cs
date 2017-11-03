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
        /// <param name="positionChecker">A function that checks if a piece is at a given position</param>
        /// <returns>The valid moves for this piece</returns>
        public virtual IEnumerable<(BoardPosition dest, MoveType outcome)> PossibleMoves(Func<BoardPosition, SpaceStatus> positionChecker)
        {
            for (var i = 0; i < _moveOffsets.Length; i++)
            {
                for (int step = 1; step <= _maxSteps; step++)
                {
                    var newX = Position.x + (_moveOffsets[i].x * step);
                    var newY = Position.y + (_moveOffsets[i].y * step);
                    var candidate = new BoardPosition(newX, newY);
                    if (ChessBoard.CheckPositionExists(candidate))
                    {
                        //Empty space => valid move and keep going
                        //Enemy => Capture is valid, but cannot jump piece
                        //Friendly => Move not valid
                        var status = positionChecker(candidate);
                        if (status == SpaceStatus.Empty)
                        {
                            yield return (candidate, MoveType.Move);
                        }
                        else if (status == SpaceStatus.Enemy)
                        {
                            yield return (candidate, MoveType.Capture);
                            break;
                        }
                        else if (status == SpaceStatus.Friendly)
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
        public virtual BoardPosition Position { get; protected set; }

        /// <summary>
        /// This should be overridden with the directions that a piece can move
        /// </summary>
        protected virtual (int x, int y)[] _moveOffsets { get; }

        /// <summary>
        /// This should be overriden with the maximum number of times that _moveOffsets 
        /// can be applied.
        /// </summary>
        protected virtual int _maxSteps { get; }
    }
}