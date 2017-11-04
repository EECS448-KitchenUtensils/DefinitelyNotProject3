using GameModel.Data;
using NUnit.Framework;
using System.Collections;
using static GameModel.Tests.TestUtils;

namespace GameModel.Tests
{
    [TestFixture]
    class ChessGameTests
    {
        //Test MakeMove works for valid moves
        //Test MakeMove fails for invalid moves
        //Test MakeMove honors which player is active
        //Test MakeMove increments the active player count on success
        //Test MakeMove loops back to Player 1 after Player 4 is done
        //Test GetPieceByPosition works for valid pieces
        //Test GetPieceByPosition returns null for invalid pieces
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
        private ChessGame _chessGame;
    }
}
