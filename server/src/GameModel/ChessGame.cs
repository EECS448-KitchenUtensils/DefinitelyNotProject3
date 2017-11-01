using System;
using System.Linq;
using System.Collections.Generic;

namespace GameModel
{
    public class ChessGame
    {
        public ChessGame()
        {
            _board = new ChessBoard();
            _playerEnums.Select(e => new ChessPlayer(e))
                        .ToArray();
            _current_player = 0;
        }
        public ChessPlayer GetActivePlayer() => _players[_current_player];
        private int _current_player;
        private ChessPlayer[] _players;
        private ChessBoard _board;
        private PlayerEnum[] _playerEnums = {PlayerEnum.PLAYER_1, PlayerEnum.PLAYER_2, PlayerEnum.PLAYER_3, PlayerEnum.PLAYER_4};
    }
}