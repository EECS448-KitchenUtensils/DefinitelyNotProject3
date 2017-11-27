using GameModel.Data;

namespace GameModel.Tests
{
    public static class TestUtils
    {
        public static BoardPosition Pos(XCoord x, int y) => new BoardPosition(x, y);
        public static MoveResult Move(BoardPosition src, BoardPosition dest, MoveType result) => new MoveResult(src, dest, result);
    }
}
