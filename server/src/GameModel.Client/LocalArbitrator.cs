using GameModel.Data;
using GameModel.Messages;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GameModel.Client
{
    /// <summary>
    /// Emulates having a connection to a remote server using only a local <see cref="ChessGame"/>
    /// </summary>
    public class LocalArbitrator : IArbitrator
    {
        /// <summary>
        /// Creates a <see cref="LocalArbitrator"/> with the default implementations
        /// of <see cref="IGameModel"/> and <see cref="ITurnController"/>
        /// </summary>
        public LocalArbitrator()
        {
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _game = new ChessGame(_tc);
            _queue = new ConcurrentQueue<ModelMessage>();
            EmitCreatePieces();
            EmitSetTurn();
        }
        
        /// <summary>
        /// Creates a <see cref="LocalArbitrator"/> with provided implementations
        /// of <see cref="IGameModel"/> and <see cref="ITurnController"/> for testing purposes
        /// </summary>
        /// <param name="tc">An <see cref="ITurnController"/> implementation or mock</param>
        /// <param name="gameModel">An <see cref="IGameModel"/> implementation or mock</param>
        public LocalArbitrator(ITurnController tc, IGameModel gameModel)
        {
            _tc = tc;
            _game = gameModel;
            _queue = new ConcurrentQueue<ModelMessage>();
            EmitCreatePieces();
            EmitSetTurn();
        }

        /// <summary>
        /// Signals that the local client would like to forfeit the game
        /// </summary>
        public void Forfeit()
        {
            _queue.Enqueue(new LostMessage(LostMessage.Reason.Forfeit, _tc.Current));
            _tc.Current.Forfeit();
        }

        /// <summary>
        /// Signals that the local client would like to move a piece
        /// </summary>
        /// <param name="src">The source <see cref="BoardPosition"/> to move a piece from</param>
        /// <param name="dest">The destination <see cref="BoardPosition"/> to move a piece to</param>
        public void MakeMove(BoardPosition src, BoardPosition dest)
        {
            var players = new[] {
                _tc.Player1,
                _tc.Player2,
                _tc.Player3,
                _tc.Player4
            };
            Player[] InGame() => players.Where(p => p.InGame)
                                            .ToArray();
            Player[] InCheck() => players.Where(p => p.InGame && p.Checked)
                                             .ToArray();
            var playersInGamePre = InGame();
            var playersInCheckPre = InCheck();
            var result = _game.MakeMove(src, dest);
            var playersInGamePost = InGame();
            var playersInCheckPost = InCheck();
            switch (result.Outcome)
            {
                case MoveType.Move:
                    EmitPieceMove(src, dest);
                    EmitLoss(playersInGamePre, playersInGamePost);
                    EmitInCheck(playersInCheckPre, playersInCheckPost);
                    EmitSetTurn();
                    break;
                case MoveType.Capture:
                    EmitPieceMove(src, dest);
                    EmitPieceDestroy(result.Destroyed);
                    EmitLoss(playersInGamePre, playersInGamePost);
                    EmitInCheck(playersInCheckPre, playersInCheckPost);
                    EmitSetTurn();
                    break;
            }
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

        /// <summary>
        /// Attempts to retrieve a <see cref="ModelMessage"/> representing a change in game state
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryGetLatestMessage(out ModelMessage message) =>
            _queue.TryDequeue(out message);

        private void EmitCreatePieces()
        {
            foreach (var piece in _game.Pieces)
            {
                var msg = new CreatePieceMessage(piece);
                _queue.Enqueue(msg);
            }
        }


        private void EmitSetTurn()
        {
            var msg = new SetTurnMessage(_tc.Current);
            _queue.Enqueue(msg);
        }


        private void EmitPieceMove(BoardPosition src, BoardPosition dest)
        {
            var msg = new TranslatePieceMessage(src, dest);
            _queue.Enqueue(msg);
        }

        private void EmitPieceDestroy(ChessPiece destroyed)
        {
            var msg = new DestroyPieceMessage(destroyed);
            _queue.Enqueue(msg);
        }

        private void EmitInCheck(Player[] playersInCheckPre, Player[] playersInCheckPost)
        {
            foreach(var newInCheck in playersInCheckPost.Except(playersInCheckPre))
            {
                var msg = new SetCheckMessage(newInCheck);
                _queue.Enqueue(msg);
            }
            foreach(var leavingCheck in playersInCheckPre.Except(playersInCheckPost))
            {
                var msg = new SetCheckMessage(leavingCheck);
                _queue.Enqueue(msg);
            }
        }

        private void EmitLoss(Player[] playersInGamePre, Player[] playersInGamePost)
        {
            foreach(var losingPlayer in playersInGamePost.Except(playersInGamePre))
            {
                var reason = losingPlayer.Checked ? LostMessage.Reason.Checkmate : LostMessage.Reason.KingCapture;
                var msg = new LostMessage(reason, losingPlayer);
                _queue.Enqueue(msg);
            }
        }

        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
