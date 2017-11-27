using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests.Pieces
{
    class KingTests: ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            Player player1 = new Player(PlayerEnum.PLAYER_1);
            var king = new King(player1, DefaultPosition);
            PossibleMovesReturnsOnlyValidMoves(king, validMoves, positionChecker);
        }

        public static BoardPosition DefaultPosition => Pos(XCoord.f, 5);

        public static IEnumerable PossibleMoveCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.g, 5), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.g, 4), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.f, 4), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 5), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Capture),
                    Move(DefaultPosition, Pos(XCoord.f, 6), MoveType.Capture)
                }, BoxChecker(Pos(XCoord.f, 5), 0, SpaceStatus.Enemy)
                ).SetName("KingPossibleMovesSurroundedByEnemies");
                yield return new TestCaseData(Pos(XCoord.f, 5), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.f, 5), 0))
                    .SetName("KingPossibleMovesSurroundedByFriends");
                yield return new TestCaseData(Pos(XCoord.f, 5), new[]
                {
                    Move(DefaultPosition, Pos(XCoord.g, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.g, 5), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.g, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.e, 4), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.e, 5), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.e, 6), MoveType.Move),
                    Move(DefaultPosition, Pos(XCoord.f, 6), MoveType.Move)
                }, EmptyChecker()
                ).SetName("KingPossibleMovesEmptySpace");
            }
        }
    }
}
