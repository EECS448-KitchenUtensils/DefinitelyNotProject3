using GameModel.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameModel
{
    /// <summary>
    /// Holds all of the pieces and cross-checks piece ownership
    /// </summary>
    internal class ChessBoard
    {
        // NOTE: constructor should make 4 players for the board to use
        internal ChessBoard(Player[] players)
        {
            _pieces = new List<ChessPiece>();
            Player1 = players[0];
            Player2 = players[1];
            Player3 = players[2];
            Player4 = players[3];
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
            if (pos.Y > _WING_WIDTH && pos.Y <= (_HEIGHT + _WING_WIDTH))
                return pos.X >= 0 && (int)pos.X < (_HEIGHT + _WING_WIDTH * 2);
            //vertical case
            if ((int)pos.X >= _WING_WIDTH && (int)pos.X < (_HEIGHT + _WING_WIDTH))
                return pos.Y >= 1 && pos.Y <= (_HEIGHT + _WING_WIDTH * 2);
            return false;
        }

        internal ChessPiece GetPieceByPosition(BoardPosition pos) =>
            _pieces.FirstOrDefault(p => p.Position == pos);

        internal IEnumerable<ChessPiece> Pieces => _pieces.AsEnumerable();

        public IEnumerable<MoveResult> PossibleMoves(BoardPosition pos)
        {
            var piece = _pieces.FirstOrDefault(p => p.Position == pos);
            if (piece == null)
                return Enumerable.Empty<MoveResult>();
            return piece.PossibleMoves((checkPos) =>
            {
                if (CheckPositionExists(checkPos) == false)
                    return SpaceStatus.Void;
                var otherPiece = _pieces.FirstOrDefault(p => p.Position == checkPos);
                if (otherPiece == null)
                    return SpaceStatus.Empty;
                else
                {
                    if (IsFriendly(piece, otherPiece))
                        return SpaceStatus.Friendly;
                    else
                        return SpaceStatus.Enemy;
                }
            });
        }

        private static bool IsFriendly(ChessPiece ourPiece, ChessPiece theirPiece)
        {
            return ourPiece.Owner == theirPiece.Owner;
        }

        /// <summary>
        /// Creates and places pieces at the correct initial positions
        /// </summary>
        private void InitBoard()
        {
            //Player 1
            _pieces.AddRange(CreatePawnsAlongLine(Player1, new BoardPosition(XCoord.d, 2), new BoardPosition(XCoord.k, 2)));
            _pieces.Add(new Rook(Player1, new BoardPosition(XCoord.d, 1)));
            _pieces.Add(new Knight(Player1, new BoardPosition(XCoord.e, 1)));
            _pieces.Add(new Bishop(Player1, new BoardPosition(XCoord.f, 1)));
            _pieces.Add(new Queen(Player1, new BoardPosition(XCoord.g, 1)));
            _pieces.Add(new King(Player1, new BoardPosition(XCoord.h, 1)));
            _pieces.Add(new Bishop(Player1, new BoardPosition(XCoord.i, 1)));
            _pieces.Add(new Knight(Player1, new BoardPosition(XCoord.j, 1)));
            _pieces.Add(new Rook(Player1, new BoardPosition(XCoord.k, 1)));
            //Player 2
            _pieces.AddRange(CreatePawnsAlongLine(Player2, new BoardPosition(XCoord.m, 4), new BoardPosition(XCoord.m, 11)));
            _pieces.Add(new Rook(Player2, new BoardPosition(XCoord.n, 4)));
            _pieces.Add(new Knight(Player2, new BoardPosition(XCoord.n, 5)));
            _pieces.Add(new Bishop(Player2, new BoardPosition(XCoord.n, 6)));
            _pieces.Add(new Queen(Player2, new BoardPosition(XCoord.n, 7)));
            _pieces.Add(new King(Player2, new BoardPosition(XCoord.n, 8)));
            _pieces.Add(new Bishop(Player2, new BoardPosition(XCoord.n, 9)));
            _pieces.Add(new Knight(Player2, new BoardPosition(XCoord.n, 10)));
            _pieces.Add(new Rook(Player2, new BoardPosition(XCoord.n, 11)));
            //Player 3
            _pieces.AddRange(CreatePawnsAlongLine(Player3, new BoardPosition(XCoord.d, 13), new BoardPosition(XCoord.k, 13)));
            _pieces.Add(new Rook(Player3, new BoardPosition(XCoord.d, 14)));
            _pieces.Add(new Knight(Player3, new BoardPosition(XCoord.e, 14)));
            _pieces.Add(new Bishop(Player3, new BoardPosition(XCoord.f, 14)));
            _pieces.Add(new King(Player3, new BoardPosition(XCoord.g, 14)));
            _pieces.Add(new Queen(Player3, new BoardPosition(XCoord.h, 14)));
            _pieces.Add(new Bishop(Player3, new BoardPosition(XCoord.i, 14)));
            _pieces.Add(new Knight(Player3, new BoardPosition(XCoord.j, 14)));
            _pieces.Add(new Rook(Player3, new BoardPosition(XCoord.k, 14)));
            //Player 4
            _pieces.AddRange(CreatePawnsAlongLine(Player4, new BoardPosition(XCoord.b, 4), new BoardPosition(XCoord.b, 11)));
            _pieces.Add(new Rook(Player4, new BoardPosition(XCoord.a, 4)));
            _pieces.Add(new Knight(Player4, new BoardPosition(XCoord.a, 5)));
            _pieces.Add(new Bishop(Player4, new BoardPosition(XCoord.a, 6)));
            _pieces.Add(new King(Player4, new BoardPosition(XCoord.a, 7)));
            _pieces.Add(new Queen(Player4, new BoardPosition(XCoord.a, 8)));
            _pieces.Add(new Bishop(Player4, new BoardPosition(XCoord.a, 9)));
            _pieces.Add(new Knight(Player4, new BoardPosition(XCoord.a, 10)));
            _pieces.Add(new Rook(Player4, new BoardPosition(XCoord.a, 11)));
        }
        /// <summary>
        /// Creates a string of pawns from point to point
        /// </summary>
        /// <param name="owner">The owner of the pawns</param>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <note>The line must be either horizontal or vertical</note>
        /// <returns></returns>
        private IEnumerable<ChessPiece> CreatePawnsAlongLine(Player owner, BoardPosition start, BoardPosition end)
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
        private Player Player1;
        private Player Player2;
        private Player Player3;
        private Player Player4;
    }
}