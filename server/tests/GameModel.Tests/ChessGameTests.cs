using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests
{
    [TestFixture]
    class ChessGameTests
    {
        //Test MakeMove loops back to Player 1 after Player 4 is done
        [SetUp]
        public void Init()
        {
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _chessGame = new ChessGame(_tc);
        }
        [Test, TestCaseSource("BoundsCheckCases")]
        public void MakeMoveBoundChecks(BoardPosition src, BoardPosition dest)
        {
            var result = _chessGame.MakeMove(src, dest);
            Assert.That(result.Outcome, Is.EqualTo(MoveType.Failure));
        }

        [Test, TestCaseSource("GetPieceCases")]
        public void GetPieceByPositionBeforeFirstMove(BoardPosition pos, bool exists, Type expectedType, Player expectedOwner)
        {
            var piece = _chessGame.GetPieceByPosition(pos);
            if (exists)
            {
                Assert.That(piece.GetType(), Is.EqualTo(expectedType));
                Assert.That(piece.Owner, Is.EqualTo(expectedOwner));
                Assert.That(piece.Position, Is.EqualTo(pos));
            }
            else
            {
                Assert.That(piece, Is.Null);
            }
        }

        [Test, TestCaseSource("MakeMoveValidMovesCases")]
        public void SingleMakeMoveValidMoves(BoardPosition src, BoardPosition dest)
        {
            var piece = _chessGame.GetPieceByPosition(src);
            var result = _chessGame.MakeMove(src, dest);
            Assert.That(result.Outcome, Is.EqualTo(MoveType.Move));
            var pieceAtDest = _chessGame.GetPieceByPosition(dest);
            Assert.That(piece.Position, Is.EqualTo(dest));
            Assert.That(pieceAtDest, Is.EqualTo(pieceAtDest));
        }

        [Test, TestCaseSource("MakeMoveInvalidMovesCases")]
        public void SingleMakeMoveInvalidMoves(BoardPosition src, BoardPosition dest, Type expectedPieceAtDest)
        {
            var piece = _chessGame.GetPieceByPosition(src);
            var result = _chessGame.MakeMove(src, dest);
            Assert.That(result.Outcome, Is.EqualTo(MoveType.Failure));
            var pieceAtDest = _chessGame.GetPieceByPosition(dest);
            Assert.That(pieceAtDest?.GetType(), Is.EqualTo(expectedPieceAtDest));
        }

        [Test]
        public void SingleValidMakeMoveIncrementsTurn()
        {
            var initialPlayer = _tc.Current;
            Assert.That(initialPlayer, Is.Not.Null);
            var result = _chessGame.MakeMove(Pos(XCoord.d, 2), Pos(XCoord.d, 4));
            Assert.That(result.Outcome, Is.EqualTo(MoveType.Move)); //Make sure this test fails if the move failed
            var resultingPlayer = _tc.Current;
            Assert.That(resultingPlayer, Is.Not.Null);
            Assert.That(resultingPlayer, Is.Not.EqualTo(initialPlayer));
        }

        [Test]
        public void MakeMoveHonorsCurrentActivePlayer()
        {
            var initialPlayer = _tc.Current;
            //Move Player 1 Pawn 1
            _chessGame.MakeMove(Pos(XCoord.d, 2), Pos(XCoord.d, 4));
            //Try to do it again
            var result = _chessGame.MakeMove(Pos(XCoord.d, 4), Pos(XCoord.d, 5));
            //Make sure player 1 was active
            Assert.That(initialPlayer, Is.EqualTo(_tc.Player1));
            Assert.That(result.Outcome, Is.EqualTo(MoveType.Failure));
        }

        public static IEnumerable BoundsCheckCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.a, 1), Pos(XCoord.a, 2))
                    .SetName("MakeMoveBoundsChecksInvalidSrcValidDest");
                yield return new TestCaseData(Pos(XCoord.a, 4), Pos(XCoord.a, 3))
                    .SetName("MakeMoveBoundsChecksValidSrcInvalidDest");
            }
        }

        public static IEnumerable GetPieceCases
        {
            get
            {
                var player1 = new Player(PlayerEnum.PLAYER_1);
                yield return new TestCaseData(Pos(XCoord.a, 1), false, typeof(ChessPiece), player1)
                    .SetName("GetPieceByPositionReturnsNullForNonexistantPiece");
                yield return new TestCaseData(Pos(XCoord.d, 2), true, typeof(Pawn), player1)
                    .SetName("GetPieceByPositionPlayer1Pawn");
            }
        }

        public static IEnumerable MakeMoveValidMovesCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.d, 2), Pos(XCoord.d, 4))
                    .SetName("MakeMoveValidPlayer1PawnFirstMove");
                yield return new TestCaseData(Pos(XCoord.e, 1), Pos(XCoord.f, 3))
                    .SetName("MakeMoveValidPlayer1LeftKnightAdvance");
            }
        }

        public static IEnumerable MakeMoveInvalidMovesCases
        {
            get
            {
                yield return new TestCaseData(Pos(XCoord.d, 2), Pos(XCoord.d, 5), null)
                    .SetName("MakeMoveValidPlayer1PawnFirstMoveTooFar");
                yield return new TestCaseData(Pos(XCoord.e, 1), Pos(XCoord.g, 2), typeof(Pawn))
                    .SetName("MakeMoveValidPlayer1LeftKnightAdvanceIntoFriendlyPawn");
                yield return new TestCaseData(Pos(XCoord.e, 1), Pos(XCoord.e, 3), null)
                    .SetName("MakeMoveValidPlayer1LeftKnightAdvanceStraightForward");
            }
        }

        private ChessGame _chessGame;
        private TurnController _tc;
    }
}
