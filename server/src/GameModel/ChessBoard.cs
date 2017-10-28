namespace GameModel
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            
        }
        /// <summary>
        /// Bounds-checks the given position
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns>true if the position exists on the board</returns>
        public bool CheckPositionExists(BoardPosition pos) =>
            (_WING_WIDTH <= (int)pos.x) && ((int)pos.x <= (_WIDTH + _WING_WIDTH)) ||
            (_WING_WIDTH <= pos.y) && (pos.y <= (_HEIGHT + _WING_WIDTH));
        const int _WING_WIDTH = 3;
        const int _HEIGHT = 8;
        const int _WIDTH = 8;
    }
}