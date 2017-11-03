using GameModel.Data;
using System.Collections.Generic;

namespace GameModel
{
    public class ChessGame
    {
        public ChessGame()
        {
            _board = new ChessBoard();
            _current_player = 0;
        }

        public IEnumerable<(BoardPosition, MoveType)> PossibleMoves(BoardPosition pos) => _board.PossibleMoves(pos);
        public PlayerEnum GetActivePlayer() => _players[_current_player];
        private int _current_player;
        private ChessBoard _board;
        private PlayerEnum[] _players = {PlayerEnum.PLAYER_1, PlayerEnum.PLAYER_2, PlayerEnum.PLAYER_3, PlayerEnum.PLAYER_4};
    }
}