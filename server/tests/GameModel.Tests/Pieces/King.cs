using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel.Tests.Pieces
{
    class KingTests: ChessPieceTests
    {
        [Test, TestCaseSource("PossibleMoveCases")]
        public void PossibleMoves(BoardPosition initialPosition, IEnumerable<MoveResult> validMoves, Func<BoardPosition, SpaceStatus> positionChecker)
        {
            var king = new King(PlayerEnum.PLAYER_1, Pos(XCoord.h, 8));
            PossibleMovesReturnsOnlyValidMoves(king, validMoves, positionChecker);
        }
        public static IEnumerable PossibleMoveCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.h, 8), new[]
                {
                    Move(XCoord.i, 9, MoveType.Capture),
                    Move(XCoord.i, 8, MoveType.Capture),
                    Move(XCoord.i, 7, MoveType.Capture),
                    Move(XCoord.h, 7, MoveType.Capture),
                    Move(XCoord.g, 7, MoveType.Capture),
                    Move(XCoord.g, 8, MoveType.Capture),
                    Move(XCoord.g, 9, MoveType.Capture),
                    Move(XCoord.h, 9, MoveType.Capture)
                }, BoxChecker(Pos(XCoord.h, 8), 0, SpaceStatus.Enemy)
                ).SetName("KingPossibleMovesSurroundedByEnemies");
                yield return new TestCaseData(Pos(XCoord.h, 8), Array.Empty<MoveResult>(), BoxChecker(Pos(XCoord.h, 8), 0))
                    .SetName("KingPossibleMovesSurroundedByFriends");
                yield return new TestCaseData(Pos(XCoord.h, 8), new[]
                {
                    Move(XCoord.i, 9, MoveType.Move),
                    Move(XCoord.i, 8, MoveType.Move),
                    Move(XCoord.i, 7, MoveType.Move),
                    Move(XCoord.h, 7, MoveType.Move),
                    Move(XCoord.g, 7, MoveType.Move),
                    Move(XCoord.g, 8, MoveType.Move),
                    Move(XCoord.g, 9, MoveType.Move),
                    Move(XCoord.h, 9, MoveType.Move)
                }, EmptyChecker()
                ).SetName("KingPossibleMovesEmptySpace");
            }
        }
        private static BoardPosition Pos(XCoord x, int y) => new BoardPosition(x, y);
        private static MoveResult Move(XCoord x, int y, MoveType result) => new MoveResult(Pos(x, y), result);
    }
}
