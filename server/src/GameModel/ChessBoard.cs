using GameModel.Data;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    public class ChessBoard
    {
        internal ChessBoard()
        {
            _pieces = new List<ChessPiece>();
            InitBoard();
        }
        /// <summary>
        /// Bounds-checks the given position
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns>true if the position exists on the board</returns>
        public static bool CheckPositionExists(BoardPosition pos)
        {
            //This works by viewing the board as the union of two rectangles, one vertical, one
            //horizontal

            //horizontal case
            if (pos.Y > _WING_WIDTH && pos.Y < (_HEIGHT + _WING_WIDTH))
                return pos.X >= 0 && (int)pos.X < (_HEIGHT + _WING_WIDTH * 2);
            //vertical case
            if ((int)pos.X >= _WING_WIDTH && (int)pos.X <= (_HEIGHT + _WING_WIDTH))
                return pos.Y >= 1 && pos.Y < (_HEIGHT + _WING_WIDTH * 2);
            return false;
        }

        //=>
        //    (_WING_WIDTH < (int)pos.X) && ((int)pos.X <= (_WIDTH + _WING_WIDTH)) &&
        //    (_WING_WIDTH < pos.Y) && (pos.Y < (_HEIGHT + _WING_WIDTH));

        internal ChessPiece GetPieceByPosition(BoardPosition pos) =>
            _pieces.FirstOrDefault(p => p.Position == pos);

        public IEnumerable<MoveResult> PossibleMoves(BoardPosition pos)
        {
            var piece = _pieces.FirstOrDefault(p => p.Position == pos);
            if (piece == null)
                return Enumerable.Empty<MoveResult>();
            return piece.PossibleMoves((checkPos) =>
            {
                if (CheckPositionExists(checkPos) == false)
                    return SpaceStatus.Void;
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
        private IEnumerable<ChessPiece> CreatePawnsAlongLine(PlayerEnum owner, BoardPosition start, BoardPosition end)
        {
            //Vertical case
            if (start.X == end.X)
            {
                for (int y = start.Y; y <= end.Y; y++)
                {
                    yield return new Pawn(owner, new BoardPosition(start.X, y));
                }
            }
            else if (start.Y == end.Y)
            {
                for(XCoord x = start.X; x <= end.X; x++)
                {
                    yield return new Pawn(owner, new BoardPosition(x, start.Y));
                }
            }
        }
        private const int _WING_WIDTH = 3;
        private const int _HEIGHT = 8;
        private const int _WIDTH = 8;
        private List<ChessPiece> _pieces;
    }
}