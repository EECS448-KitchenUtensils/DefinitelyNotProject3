using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using UnityEngine;
using WebSocketSharp;

namespace GameModel.Client
{
    /// <summary>
    /// Synchronizes a <see cref="ChessGame"/> over the network by connecting to a remote server
    /// </summary>
    public class NetworkArbitrator : IArbitrator
    {
        /// <summary>
        /// Creates and starts a new <see cref="NetworkArbitrator"/>
        /// </summary>
        /// <param name="wsAddress">Address of the server</param>
        public NetworkArbitrator(string wsAddress)
        {
            _ws = new WebSocket(wsAddress);
            CreateSerializer();
            _th = new Thread(Connect);
            _th.Start();
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _game = new ChessGame(_tc);
            _queue = new ConcurrentQueue<ModelMessage>();
        }

        /// <summary>
        /// Connects to the server and begins receiving messages
        /// </summary>
        public void Connect()
        {
            _ws.Connect();
            CheckForMessages();
            Debug.Log("Connected");
        }

        private void CreateSerializer()
        {
            var knownTypes = new[]
            {
                typeof(CreatePieceMessage),
                typeof(TranslatePieceMessage),
                typeof(GameBeginMessage),
                typeof(DestroyPieceMessage),
                typeof(SetTurnMessage),
                typeof(PieceEnum),
                typeof(PlayerEnum),
                typeof(XCoord)
            };

            _serializer = new DataContractSerializer(typeof(ModelMessage), knownTypes);
        }

        /// <summary>
        /// Signals that the local client would like to forfeit the game
        /// </summary>
        public void Forfeit()
        {
            //Nothing rn
        }

        /// <summary>
        /// Signals that the local client would like to move a piece on the board
        /// </summary>
        /// <param name="src">Source position to move a piece from</param>
        /// <param name="dest">Destination position of the piece</param>
        public void MakeMove(BoardPosition src, BoardPosition dest)
        {

            TranslatePieceMessage moveToTry = new TranslatePieceMessage(src, dest);
            var stream = new MemoryStream();
            _serializer.WriteObject(stream, moveToTry);
            _ws.Send(stream.ToArray());
           
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
        /// Attempts to get a message that has been received from the server
        /// </summary>
        /// <param name="message">Message received from server (out variable)</param>
        /// <returns>true if a message was successfully retrieved</returns>
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
            foreach (var newInCheck in playersInCheckPost.Except(playersInCheckPre))
            {
                var msg = new SetCheckMessage(newInCheck);
                _queue.Enqueue(msg);
            }
            foreach (var leavingCheck in playersInCheckPre.Except(playersInCheckPost))
            {
                var msg = new SetCheckMessage(leavingCheck);
                _queue.Enqueue(msg);
            }
        }

        private void EmitLoss(Player[] playersInGamePre, Player[] playersInGamePost)
        {
            foreach (var losingPlayer in playersInGamePost.Except(playersInGamePre))
            {
                var reason = losingPlayer.Checked ? LostMessage.Reason.Checkmate : LostMessage.Reason.KingCapture;
                var msg = new LostMessage(reason, losingPlayer);
                _queue.Enqueue(msg);
            }
        }

        private void CheckForMessages()
        {
            Debug.Log("Check for Messages");
            _ws.OnMessage += (sender, e) => ParseMessage(sender, e);
        }
        
        private void ParseMessage(object sender, MessageEventArgs e)
        {
            var recievedStream = new MemoryStream(e.RawData);
            var recievedObject = (ModelMessage)_serializer.ReadObject(recievedStream);
            if (recievedObject is TranslatePieceMessage)
            {
                var message = (TranslatePieceMessage)recievedObject;
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
                var result = _game.MakeMove(message.src, message.dest);
                var playersInGamePost = InGame();
                var playersInCheckPost = InCheck();
            }
            _queue.Enqueue(recievedObject);
        }

        private Thread _th;
        private DataContractSerializer _serializer;
        private WebSocket _ws;
        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
