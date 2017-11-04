using System;
using System.Collections.Generic;
using GameModel.Data;
using NUnit.Framework;

namespace GameModel.Tests.Pieces
{
    class ChessPieceTests
    {
        /// <summary>
        /// Returns a positionChecker that assumes the board is completely empty
        /// </summary>
        /// <returns></returns>
        protected static Func<BoardPosition, SpaceStatus> EmptyChecker()
        {
            SpaceStatus positionChecker(BoardPosition pos)
            {
                if (BoundsCheck(pos))
                    return SpaceStatus.Empty;
                else
                    return SpaceStatus.Void;
            }
            return positionChecker;
        }

        /// <summary>
        /// Returns a positionChecker that simulates the piece surround by a ring of other pieces
        /// </summary>
        /// <param name="piecePosition">The position of the piece under test</param>
        /// <param name="boxRadius">How many spaces between the ring and the piece under test</param>
        /// <param name="boxType">What type of pieces the ring is composed of</param>
        /// <returns></returns>
        protected static Func<BoardPosition, SpaceStatus> BoxChecker(BoardPosition piecePosition, int boxRadius, SpaceStatus boxType = SpaceStatus.Friendly)
        {
            SpaceStatus positionChecker(BoardPosition pos)
            {
                if (BoundsCheck(pos))
                {
                    var xDistance = Math.Abs(piecePosition.X - pos.X);
                    var yDistance = Math.Abs(piecePosition.Y - pos.Y);
                    if (xDistance > boxRadius || yDistance > boxRadius)
                        return boxType;
                    else
                        return SpaceStatus.Empty;
                }
                else
                    return SpaceStatus.Void;
            }
            return positionChecker;
        }

        private static bool BoundsCheck(BoardPosition pos) =>
            (pos.X >= XCoord.a && pos.X <= XCoord.h) && (pos.Y >= 1 && pos.Y <= 8);

        /// <summary>
        /// Helper method that allows the tests to generalized
        /// </summary>
        /// <param name="piece">A piece instance at some position</param>
        /// <param name="expectedMoves"></param>
        /// <param name="positionChecker"></param>
        protected void PossibleMovesReturnsOnlyValidMoves(ChessPiece piece, IEnumerable<MoveResult> expectedMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var calculatedMoves = new HashSet<MoveResult>(piece.PossibleMoves(positionChecker));
            Assert.That(() => calculatedMoves.SetEquals(expectedMoves), Is.True);
        }
    }
}
