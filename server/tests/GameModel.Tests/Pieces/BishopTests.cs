using System;
using GameModel.Data;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace GameModel.Tests.Pieces
{
    class BishopTests : ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var bishop = new Bishop(PlayerEnum.PLAYER_1, Pos(XCoord.f, 5));
            PossibleMovesReturnsOnlyValidMoves(bishop, validMoves, positionChecker);
        }

        public static IEnumerable PossibleMoveCases
        {
            get
            {
                // empty paths
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    // upper-left diagonal
                    Move(XCoord.e, 6, MoveType.Move),
                    Move(XCoord.d, 7, MoveType.Move),
                    Move(XCoord.c, 8, MoveType.Move),
                    Move(XCoord.b, 9, MoveType.Move),
                    Move(XCoord.a, 10, MoveType.Move),
                    // upper-right diagonal
                    Move(XCoord.g, 6, MoveType.Move),
                    Move(XCoord.h, 7, MoveType.Move),
                    Move(XCoord.i, 8, MoveType.Move),
                    Move(XCoord.j, 9, MoveType.Move),
                    Move(XCoord.k, 10, MoveType.Move),
                    Move(XCoord.l, 11, MoveType.Move),
                    // lower-right diagonal
                    Move(XCoord.g, 4, MoveType.Move),
                    Move(XCoord.h, 3, MoveType.Move),
                    Move(XCoord.i, 2, MoveType.Move),
                    Move(XCoord.j, 1, MoveType.Move),
                    // lower-left diagonal
                    Move(XCoord.e, 4, MoveType.Move),
                    Move(XCoord.d, 3, MoveType.Move)
                }, EmptyChecker()
                ).SetName("BishopPossibleMovesEmptySpace");

                // enclosed by friendly pieces
                yield return new TestCaseData(Pos(XCoord.f, 5), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.f, 5), 0))
                    .SetName("BishopPossibleMovesSurroundedByFriends");

                // enclosed by enemy pieces
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    // upper-left diagonal
                    Move(XCoord.e, 6, MoveType.Capture),
                    // upper-right diagonal
                    Move(XCoord.g, 6, MoveType.Capture),
                    // lower-right diagonal
                    Move(XCoord.g, 4, MoveType.Capture),
                    // lower-left diagonal
                    Move(XCoord.e, 4, MoveType.Capture),
                }, BoxChecker(Pos(XCoord.f, 5), 0, SpaceStatus.Enemy)
                ).SetName("BishopPossibleMovesSurroundedByEnemies");
            }
        }
    }
}
