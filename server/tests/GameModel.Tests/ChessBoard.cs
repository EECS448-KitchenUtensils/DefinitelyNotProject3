using NUnit.Framework;
using GameModel;
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
        public void CheckPositionExists(XCoord x, int y, bool inBounds)
        {
            var pos = new BoardPosition(x, y);
            Assert.AreEqual(ChessBoard.CheckPositionExists(pos), inBounds);
        }
    }
}