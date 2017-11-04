using GameModel.Data;

namespace GameModel.Tests
{
    public static class TestUtils
    {
        public static BoardPosition Pos(XCoord x, int y) => new BoardPosition(x, y);
        public static MoveResult Move(XCoord x, int y, MoveType result) => new MoveResult(Pos(x, y), result);
    }
}
