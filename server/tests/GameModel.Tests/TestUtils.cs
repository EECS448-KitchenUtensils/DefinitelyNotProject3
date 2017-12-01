using GameModel.Data;

namespace GameModel.Tests
{
    /// <summary>
    /// Contains helper methods for testing other classes
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Creates a <see cref="BoardPosition"/>
        /// </summary>
        /// <param name="x">File</param>
        /// <param name="y">Rank</param>
        /// <returns>A new <see cref="BoardPosition"/> instance</returns>
        public static BoardPosition Pos(XCoord x, int y) => new BoardPosition(x, y);

        /// <summary>
        /// Creates a new <see cref="MoveResult"/>
        /// </summary>
        /// <param name="src">Source position</param>
        /// <param name="dest">Destination position</param>
        /// <param name="result">Expected result of the move</param>
        /// <returns>A fresh <see cref="MoveResult"/> instance</returns>
        public static MoveResult Move(BoardPosition src, BoardPosition dest, MoveType result) => new MoveResult(src, dest, result);
    }
}
