using GameModel.Data;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            _pieces = new List<ChessPiece>();
            InitBoard();

        }
        /// <summary>
        /// Bounds-checks the given position
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns>true if the position exists on the board</returns>
        public static bool CheckPositionExists(BoardPosition pos) =>
            (_WING_WIDTH <= (int)pos.x) && ((int)pos.x <= (_WIDTH + _WING_WIDTH)) ||
            (_WING_WIDTH <= pos.y) && (pos.y <= (_HEIGHT + _WING_WIDTH));

        public IEnumerable<(BoardPosition, MoveType)> PossibleMoves(BoardPosition pos)
        {
            var piece = _pieces.FirstOrDefault(p => p.Position == pos);
            if (piece == null)
                return Enumerable.Empty<(BoardPosition, MoveType)>();
            return piece.PossibleMoves((checkPos) =>
            {
                var checkSpace = _pieces.FirstOrDefault(p => p.Position == checkPos);
                if (checkSpace == null)
                    return SpaceStatus.Empty;
                else
                {
                    if ((int)(checkSpace.Owner) % 2 == ((int)piece.Owner % 2))
                    {
                        //Same team
                        return SpaceStatus.Friendly;
                    }
                    else
                        return SpaceStatus.Enemy;
                }
            });
        }

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

        /// <summary>
        /// Creates and places pieces at the correct initial positions
        /// </summary>
        private void InitBoard()
        {
            //Player 1
            _pieces.AddRange(CreatePawnsAlongLine(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.d, 2), new BoardPosition(XCoord.k, 2)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.d, 1)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.e, 1)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.f, 1)));
            _pieces.Add(new Queen(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.g, 1)));
            _pieces.Add(new King(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.h, 1)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.i, 1)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.j, 1)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_1, new BoardPosition(XCoord.k, 1)));
            //Player 2
            _pieces.AddRange(CreatePawnsAlongLine(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.m, 4), new BoardPosition(XCoord.m, 11)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 4)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 5)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 6)));
            _pieces.Add(new Queen(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 7)));
            _pieces.Add(new King(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 8)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 9)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 10)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_2, new BoardPosition(XCoord.n, 11)));
            //Player 3
            _pieces.AddRange(CreatePawnsAlongLine(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.d, 13), new BoardPosition(XCoord.d, 13)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.d, 14)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.e, 14)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.f, 14)));
            _pieces.Add(new King(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.g, 14)));
            _pieces.Add(new Queen(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.h, 14)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.i, 14)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.j, 14)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_3, new BoardPosition(XCoord.k, 14)));
            //Player 4
            _pieces.AddRange(CreatePawnsAlongLine(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.b, 4), new BoardPosition(XCoord.b, 11)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 4)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 5)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 6)));
            _pieces.Add(new King(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 7)));
            _pieces.Add(new Queen(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 8)));
            _pieces.Add(new Bishop(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 9)));
            _pieces.Add(new Knight(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 10)));
            _pieces.Add(new Rook(PlayerEnum.PLAYER_4, new BoardPosition(XCoord.a, 11)));
        }
        /// <summary>
        /// Creates a string of pawns from point to point
        /// </summary>
        /// <param name="owner">The owner of the pawns</param>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <note>The line must be either horizontal or vertical</note>
        /// <returns></returns>
        private IEnumerable<Pawn> CreatePawnsAlongLine(PlayerEnum owner, BoardPosition start, BoardPosition end)
        {
            //Vertical case
            if (start.x == end.x)
            {
                for (int y = start.y; y <= end.y; y++)
                {
                    yield return new Pawn(owner, new BoardPosition(start.x, y));
                }
            }
            else if (start.y == end.y)
            {
                for(XCoord x = start.x; x <= end.x; x++)
                {
                    yield return new Pawn(owner, new BoardPosition(x, start.y));
                }
            }
        }
        private const int _WING_WIDTH = 3;
        private const int _HEIGHT = 8;
        private const int _WIDTH = 8;
        private List<ChessPiece> _pieces;
    }
}