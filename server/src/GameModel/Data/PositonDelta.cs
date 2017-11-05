namespace GameModel.Data
{
    /// <summary>
    /// Holds a position difference for translating a piece on the board
    /// </summary>
    public struct PositionDelta 
    {
        /// <summary>
        /// Initializes a PositionDelta
        /// </summary>
        /// <param name="xDiff">The difference along the horizontal axis</param>
        /// <param name="yDiff">The difference along the vertical axis</param>
        public PositionDelta(int xDiff, int yDiff)
        {
            X = xDiff;
            Y = yDiff;
        }

        /// <summary>
        /// Returns a deterministic hash code based on the values of this PositionDelta
        /// </summary>
        /// <returns>The hash code of this PositionDelta</returns>
        public override int GetHashCode() => X ^ Y;

        /// <summary>
        /// The horizontal offset
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The vertical offset
        /// </summary>
        public int Y { get; }
    }
}