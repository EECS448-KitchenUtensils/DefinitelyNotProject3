using System.Collections.Generic;
using System;
using LanguageExt;

namespace GameModel
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            _pieces = new Dictionary<BoardPosition, ChessPiece>();
        }
        /// <summary>
        /// Bounds-checks the given position
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns>true if the position exists on the board</returns>
        public static bool CheckPositionExists(BoardPosition pos) =>
            (_WING_WIDTH <= (int)pos.x) && ((int)pos.x <= (_WIDTH + _WING_WIDTH)) ||
            (_WING_WIDTH <= pos.y) && (pos.y <= (_HEIGHT + _WING_WIDTH));
        
        /// <summary>
        /// Finds a piece by its position on the board
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns>Either a ChessPiece or None</returns>
        internal Option<ChessPiece> PieceAtPosition(BoardPosition pos)
        {
            if (_pieces.TryGetValue(pos, out var piece))
                return Option<ChessPiece>.Some(piece);
            else
                return Option<ChessPiece>.None;
        }

        /// <summary>
        /// Attempts to move a piece on the board, with no rules checking
        /// </summary>
        /// <param name="src">The position the piece is currently in</param>
        /// <param name="dest">The desired destination position</param>
        /// <returns>The destination position if the operation succeeds</returns>
        internal bool Translate(BoardPosition src, BoardPosition dest)
        {
            if (_pieces.ContainsKey(src) && !(_pieces.ContainsKey(dest))) {
                _pieces[dest] = _pieces[src];
                _pieces.Remove(src);
                return true;
            } else {
                return false;
            }
        }
        private const int _WING_WIDTH = 3;
        private const int _HEIGHT = 8;
        private const int _WIDTH = 8;
        private IDictionary<BoardPosition, ChessPiece> _pieces;
    }
}