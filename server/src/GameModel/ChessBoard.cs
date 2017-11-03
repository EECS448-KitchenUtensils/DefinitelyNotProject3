using System.Collections.Generic;
using System;
using LanguageExt;
using System.Linq;

namespace GameModel
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            _pieces = new List<ChessPiece>();

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
        /// Attempts to move a piece on the board, with no rules checking
        /// </summary>
        /// <param name="src">The position the piece is currently in</param>
        /// <param name="dest">The desired destination position</param>
        /// <returns>The destination position if the operation succeeds</returns>
        private bool Translate(BoardPosition src, BoardPosition dest, bool isCapturing = false)
        {
            //bail early if either position doesn't exist
            if (!CheckPositionExists(src) || !CheckPositionExists(dest))
                return false;
            //check to make sure that src piece exists
            var srcPiece = _pieces.FirstOrDefault(piece => piece.Position == src);
            if (srcPiece == null)
                return false;
            var destPiece = _pieces.FirstOrDefault(piece => piece.Position == dest);
            if (!isCapturing)
            {
                //if we aren't capturing, then moving to an occupied piece is invalid
                if (destPiece == null)
                {
                    srcPiece.Position = dest;
                    return true;
                }
                return false;
            }
            else
            {
                if (destPiece != null)
                    _pieces.Remove(destPiece);
                srcPiece.Position = dest;
                return true;
            }
        }
        private const int _WING_WIDTH = 3;
        private const int _HEIGHT = 8;
        private const int _WIDTH = 8;
        private List<ChessPiece> _pieces;
    }
}