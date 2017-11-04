using System;
using System.Collections.Generic;
using GameModel.Data;
using NUnit.Framework;

namespace GameModel.Tests.Pieces
{
    class ChessPieceTests
    {
        /// <summary>
        /// Checks what a piece does on an empty board
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="expectedMoves"></param>
        protected void PossibleMovesReturnsOnlyValidMovesInEmptySpace(ChessPiece piece, ISet<MoveResult> expectedMoves)
        {
            SpaceStatus positionChecker(BoardPosition pos)
            {
                if (ChessBoard.CheckPositionExists(pos))
                    return SpaceStatus.Empty;
                else
                    return SpaceStatus.Void;
            }
            PossibleMovesReturnsOnlyValidMoves(piece, expectedMoves, positionChecker);
        }

        protected void PossibleMovesInABox(ChessPiece piece, int boxRadius, SpaceStatus boxType, ISet<MoveResult> expectedMoves)
        {
            SpaceStatus positionChecker(BoardPosition pos)
            {
                if (ChessBoard.CheckPositionExists(pos))
                {
                    var xDistance = Math.Abs(piece.Position.X - pos.X);
                    var yDistance = Math.Abs(piece.Position.Y - pos.Y);
                    if (xDistance > boxRadius || yDistance > boxRadius)
                        return boxType;
                    else
                        return SpaceStatus.Empty;
                }
                else
                    return SpaceStatus.Void;
            }
            PossibleMovesReturnsOnlyValidMoves(piece, expectedMoves, positionChecker);
        }

        /// <summary>
        /// Helper method that allows the tests to generalized
        /// </summary>
        /// <param name="piece">A piece instance at some position</param>
        /// <param name="expectedMoves"></param>
        /// <param name="positionChecker"></param>
        private void PossibleMovesReturnsOnlyValidMoves(ChessPiece piece, ISet<MoveResult> expectedMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var calculatedMoves = piece.PossibleMoves(positionChecker);
            Assert.That(() => expectedMoves.SetEquals(calculatedMoves), Is.True);
        }
    }
}
