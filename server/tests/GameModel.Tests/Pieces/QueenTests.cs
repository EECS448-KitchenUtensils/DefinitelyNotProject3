using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameModel.Tests.Pieces
{
    class QueenTests: ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var queen = new Queen(PlayerEnum.PLAYER_1, Pos(XCoord.d, 4));
            PossibleMovesReturnsOnlyValidMoves(queen, validMoves, positionChecker);
        }
        public static IEnumerable PossibleMoveCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.d, 4), new[]
                {
                    Move(XCoord.e, 5, MoveType.Capture),
                    Move(XCoord.e, 4, MoveType.Capture),
                    Move(XCoord.e, 3, MoveType.Capture),
                    Move(XCoord.d, 3, MoveType.Capture),
                    Move(XCoord.c, 3, MoveType.Capture),
                    Move(XCoord.c, 4, MoveType.Capture),
                    Move(XCoord.c, 5, MoveType.Capture),
                    Move(XCoord.d, 5, MoveType.Capture)
                }, BoxChecker(Pos(XCoord.d, 4), 0, SpaceStatus.Enemy)
                ).SetName("QueenPossibleMovesSurroundedByEnemies");
                yield return new TestCaseData(Pos(XCoord.d, 4), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.d, 4), 0))
                    .SetName("QueenPossibleMovesSurroundedByFriends");
                yield return new TestCaseData(Pos(XCoord.d, 4), new[]
                {
                    //1
                    Move(XCoord.e, 5, MoveType.Move),
                    Move(XCoord.f, 6, MoveType.Move),
                    Move(XCoord.g, 7, MoveType.Move),
                    Move(XCoord.h, 8, MoveType.Move),
                    //2
                    Move(XCoord.e, 4, MoveType.Move),
                    Move(XCoord.f, 4, MoveType.Move),
                    Move(XCoord.g, 4, MoveType.Move),
                    Move(XCoord.h, 4, MoveType.Move),
                    //3
                    Move(XCoord.e, 3, MoveType.Move),
                    Move(XCoord.f, 2, MoveType.Move),
                    Move(XCoord.g, 1, MoveType.Move),
                    //4
                    Move(XCoord.d, 3, MoveType.Move),
                    Move(XCoord.d, 2, MoveType.Move),
                    Move(XCoord.d, 1, MoveType.Move),
                    //5
                    Move(XCoord.c, 3, MoveType.Move),
                    Move(XCoord.b, 2, MoveType.Move),
                    Move(XCoord.a, 1, MoveType.Move),
                    //6
                    Move(XCoord.c, 4, MoveType.Move),
                    Move(XCoord.b, 4, MoveType.Move),
                    Move(XCoord.a, 4, MoveType.Move),
                    //7
                    Move(XCoord.c, 5, MoveType.Move),
                    Move(XCoord.b, 6, MoveType.Move),
                    Move(XCoord.a, 7, MoveType.Move),
                    //8
                    Move(XCoord.d, 5, MoveType.Move),
                    Move(XCoord.d, 6, MoveType.Move),
                    Move(XCoord.d, 7, MoveType.Move),
                    Move(XCoord.d, 8, MoveType.Move)
                }, EmptyChecker()
                ).SetName("QueenPossibleMovesEmptySpace");
            }
        }
        private static BoardPosition Pos(XCoord x, int y) => new BoardPosition(x, y);
        private static MoveResult Move(XCoord x, int y, MoveType result) => new MoveResult(Pos(x, y), result);
    }
}
