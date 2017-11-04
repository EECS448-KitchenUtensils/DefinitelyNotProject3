﻿using GameModel.Data;
using NUnit.Framework;
using System;
using System.Collections;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests
{
    [TestFixture]
    class ChessGameTests
    {
        //Test MakeMove works for valid moves
        //Test MakeMove fails for invalid moves
        //Test MakeMove loops back to Player 1 after Player 4 is done
        [SetUp]
        public void Init()
        {
            _chessGame = new ChessGame();
        }
        [Test, TestCaseSource("BoundsCheckCases")]
        public void MakeMoveBoundChecks(BoardPosition src, BoardPosition dest)
        {
            var result = _chessGame.MakeMove(src, dest);
            Assert.That(result, Is.EqualTo(MoveType.Failure));
        }

        [Test, TestCaseSource("GetPieceCases")]
        public void GetPieceByPositionBeforeFirstMove(BoardPosition pos, bool exists, Type expectedType, PlayerEnum expectedOwner)
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
            Assert.That(result, Is.EqualTo(MoveType.Move));
            var pieceAtDest = _chessGame.GetPieceByPosition(dest);
            Assert.That(pieceAtDest, Is.EqualTo(pieceAtDest));
        }

        [Test]
        public void SingleValidMakeMoveIncrementsTurn()
        {
            var initialPlayer = _chessGame.GetActivePlayer();
            Assert.That(initialPlayer, Is.EqualTo(PlayerEnum.PLAYER_1));
            var result = _chessGame.MakeMove(Pos(XCoord.d, 2), Pos(XCoord.d, 4));
            Assert.That(result, Is.EqualTo(MoveType.Move)); //Make sure this test fails if the move failed
            var resultingPlayer = _chessGame.GetActivePlayer();
            Assert.That(resultingPlayer, Is.EqualTo(PlayerEnum.PLAYER_2));
        }

        [Test]
        public void MakeMoveHonorsCurrentActivePlayer()
        {
            var initialPlayer = _chessGame.GetActivePlayer();
            //Move Player 1 Pawn 1
            _chessGame.MakeMove(Pos(XCoord.d, 2), Pos(XCoord.d, 4));
            //Try to do it again
            var result = _chessGame.MakeMove(Pos(XCoord.d, 4), Pos(XCoord.d, 5));
            //Make sure player 1 was active
            Assert.That(initialPlayer, Is.EqualTo(PlayerEnum.PLAYER_1));
            Assert.That(result, Is.EqualTo(MoveType.Failure));
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
                yield return new TestCaseData(Pos(XCoord.a, 1), false, typeof(ChessPiece), PlayerEnum.PLAYER_1)
                    .SetName("GetPieceByPositionReturnsNullForNonexistantPiece");
                yield return new TestCaseData(Pos(XCoord.d, 2), true, typeof(Pawn), PlayerEnum.PLAYER_1)
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

        private ChessGame _chessGame;
    }
}
