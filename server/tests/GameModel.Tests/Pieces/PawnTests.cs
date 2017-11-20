using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests.Pieces
{
    class PawnTests : ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker, bool hasMovedYet)
        {
            Pawn pawn;
            Player player1 = new Player(PlayerEnum.PLAYER_1);

            //Simulate a previous move
            if (hasMovedYet)
            {
                pawn = new Pawn(player1, DefaultPositionPreMove);
                pawn.Position = DefaultPosition;
            }
            else
            {
                pawn = new Pawn(player1, DefaultPosition);
            }
            PossibleMovesReturnsOnlyValidMoves(pawn, validMoves, positionChecker);
        }

        public static BoardPosition DefaultPosition => Pos(XCoord.f, 5);
        public static BoardPosition DefaultPositionPreMove => Pos(XCoord.f, 4);

        public static IEnumerable PossibleMoveCases
        {
            get
            {
                yield return new TestCaseData(DefaultPosition, new[]
                {
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Capture)
                }, BoxChecker(Pos(XCoord.f, 5), 0, SpaceStatus.Enemy), false
                ).SetName("PawnPossibleMovesSurroundedByEnemiesFirstMove");
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Capture)
                }, BoxChecker(Pos(XCoord.f, 5), 0, SpaceStatus.Enemy), true
                ).SetName("PawnPossibleMovesSurroundedByEnemiesNotFirstMove");
                yield return new TestCaseData(Pos(XCoord.f, 5), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.f, 5), 0), false)
                    .SetName("PawnPossibleMovesSurroundedByFriendsFirstMove");
                yield return new TestCaseData(Pos(XCoord.f, 5), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.f, 5), 0), true)
                    .SetName("PawnPossibleMovesSurroundedByFriendsNotFirstMove");
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    Move(DefaultPosition, Pos(XCoord.f, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 7), MoveType.Move)
                }, EmptyChecker(), false
                ).SetName("PawnPossibleMovesEmptySpaceFirstMove");
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    Move(DefaultPosition, Pos(XCoord.f, 6), MoveType.Move)
                }, EmptyChecker(), true
                ).SetName("PawnPossibleMovesEmptySpaceNotFirstMove");
            }
        }
    }
}
