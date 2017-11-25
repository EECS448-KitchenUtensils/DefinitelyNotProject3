using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GameModel.Client
{
    public class LocalArbitrator : IArbitrator
    {
        public LocalArbitrator()
        {
            _game = new ChessGame();
            _queue = new ConcurrentQueue<ModelMessage>();
        }

        public void Forfeit(Player player)
        {
            throw new NotImplementedException();
        }

        public void MakeMove(BoardPosition src, BoardPosition dest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Proxies <see cref="ChessGame.PossibleMoves(BoardPosition)"/>
        /// </summary>
        /// <param name="pos">Position to check from</param>
        /// <returns>A sequence of possible moves</returns>
        public IEnumerable<MoveResult> PossibleMoves(BoardPosition pos) =>
            _game.PossibleMoves(pos);

        /// <summary>
        /// Fakes a shutdown since that doesn't really make sense for local games
        /// </summary>
        public void Shutdown()
        {
        }

        public bool TryGetLatestMessage(out ModelMessage message) =>
            _queue.TryDequeue(out message);

        private ChessGame _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
