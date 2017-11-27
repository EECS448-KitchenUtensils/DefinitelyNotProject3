using System;
using GameModel.Data;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests.Pieces
{
    class BishopTests : ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var player1 = new Player(PlayerEnum.PLAYER_1);
            var bishop = new Bishop(player1, Pos(XCoord.f, 5));
            PossibleMovesReturnsOnlyValidMoves(bishop, validMoves, positionChecker);
        }

        public static BoardPosition DefaultPosition => Pos(XCoord.f, 5);

        public static IEnumerable PossibleMoveCases
        {
            get
            {
                // empty paths
                yield return new TestCaseData(DefaultPosition, new[]
                {
                    // upper-left diagonal
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 7), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.c, 8), MoveType.Move),
                    // upper-right diagonal
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.h, 7), MoveType.Move),
                    // lower-right diagonal
                    Move(DefaultPosition, Pos(XCoord.g, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.h, 3), MoveType.Move),
                    // lower-left diagonal
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 3), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.c, 2), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.b, 1), MoveType.Move)
                }, EmptyChecker()
                ).SetName("BishopPossibleMovesEmptySpace");

                // enclosed by friendly pieces
                yield return new TestCaseData(Pos(XCoord.f, 5), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.f, 5), 0))
                    .SetName("BishopPossibleMovesSurroundedByFriends");

                // enclosed by enemy pieces
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    // upper-left diagonal
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Capture),
                    // upper-right diagonal
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Capture),
                    // lower-right diagonal
                    Move(DefaultPosition, Pos(XCoord.g, 4), MoveType.Capture),
                    // lower-left diagonal
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Capture),
                }, BoxChecker(Pos(XCoord.f, 5), 0, SpaceStatus.Enemy)
                ).SetName("BishopPossibleMovesSurroundedByEnemies");
            }
        }
    }
}
