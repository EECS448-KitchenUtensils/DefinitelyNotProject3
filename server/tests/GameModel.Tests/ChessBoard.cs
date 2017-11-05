using NUnit.Framework;
using GameModel.Data;

namespace GameModel.Tests
{
    [TestFixture]
    class ChessBoardTests
    {
        [TestCase(XCoord.a, 0, false)] //Lower left corner
        [TestCase(XCoord.n, 0, false)] //Lower right corner
        [TestCase(XCoord.a, 13, false)] //Upper left corner
        [TestCase(XCoord.n, 13, false)] //Upper right corner
        [TestCase(XCoord.f, 6, true)] //Roughly middle
        [TestCase(XCoord.a, 7, true)] //Left wing
        [TestCase(XCoord.n, 7, true)] //Right wing
        [TestCase(XCoord.f, 12, true)] //Top wing
        [TestCase(XCoord.f, 02, true)] //Bottom wing
        [TestCase(XCoord.a, 3, false)] //top outside edge of lower left cutout
        [TestCase(XCoord.c, 2, false)] //right outside edge of lower left cutout
        [TestCase(XCoord.l, 2, false)] //left outside edge of lower right cutout
        [TestCase(XCoord.m, 12, false)] //lower outside edge of upper right cutout
        [TestCase(XCoord.d, 1, true)] //Player 1 rook 1
        [TestCase(XCoord.k, 1, true)] //Player 1 rook 2
        [TestCase(XCoord.n, 4, true)] //Player 2 rook 1
        [TestCase(XCoord.n, 11, true)] //Player 2 rook 2
        [TestCase(XCoord.k, 14, true)] //Player 3 rook 1
        [TestCase(XCoord.d, 14, true)] //Player 3 rook 2
        [TestCase(XCoord.a, 11, true)] //Player 4 rook 1
        [TestCase(XCoord.a, 4, true)] //Player 4 rook 2
        public void CheckPositionExists(XCoord x, int y, bool inBounds)
        {
            var pos = new BoardPosition(x, y);
            Assert.AreEqual(ChessBoard.CheckPositionExists(pos), inBounds);
        }
    }
}