using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests.Pieces
{
    class QueenTests: ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            Player player1 = new Player(PlayerEnum.PLAYER_1);
            var queen = new Queen(player1, Pos(XCoord.d, 4));
            PossibleMovesReturnsOnlyValidMoves(queen, validMoves, positionChecker);
        }

        public static BoardPosition DefaultPosition => Pos(XCoord.d, 4);

        public static IEnumerable PossibleMoveCases
        {
            get
            {
                yield return new TestCaseData(DefaultPosition, new[]
                {
                    Move(DefaultPosition, Pos(XCoord.e, 5), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 3), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.d, 3), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.c, 3), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.c, 4), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.c, 5), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.d, 5), MoveType.Capture)
                }, BoxChecker(Pos(XCoord.d, 4), 0, SpaceStatus.Enemy)
                ).SetName("QueenPossibleMovesSurroundedByEnemies");
                yield return new TestCaseData(Pos(XCoord.d, 4), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.d, 4), 0))
                    .SetName("QueenPossibleMovesSurroundedByFriends");
                yield return new TestCaseData(Pos(XCoord.d, 4), new[]
                {
                    //1
                    Move(DefaultPosition, Pos(XCoord.e, 5), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.g, 7), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.h, 8), MoveType.Move),
                    //2
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.g, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.h, 4), MoveType.Move),
                    //3
                    Move(DefaultPosition, Pos(XCoord.e, 3), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 2), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.g, 1), MoveType.Move),
                    //4
                    Move(DefaultPosition, Pos(XCoord.d, 3), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 2), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 1), MoveType.Move),
                    //5
                    Move(DefaultPosition, Pos(XCoord.c, 3), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.b, 2), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.a, 1), MoveType.Move),
                    //6
                    Move(DefaultPosition, Pos(XCoord.c, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.b, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.a, 4), MoveType.Move),
                    //7
                    Move(DefaultPosition, Pos(XCoord.c, 5), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.b, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.a, 7), MoveType.Move),
                    //8
                    Move(DefaultPosition, Pos(XCoord.d, 5), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 7), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.d, 8), MoveType.Move)
                }, EmptyChecker()
                ).SetName("QueenPossibleMovesEmptySpace");
            }
        }
    }
}
