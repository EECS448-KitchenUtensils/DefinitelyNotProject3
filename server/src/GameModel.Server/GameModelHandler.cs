using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameModel.Server
{
    /// <summary>
    /// Handles a connection with a client GameModel over a websocket
    /// </summary>
    public class GameModelHandler : WebSocketBehavior
    {
        /// <summary>
        /// Called when a connection is opened
        /// </summary>
        protected override void OnOpen()
        {
            Debug.WriteLine($"Got a connection from: {Context.UserEndPoint}");
            _running = true;
            var msgStream = Observable.FromEventPattern<GameMessageEventArgs>(
                handler => _messages += handler,
                handler => _messages -= handler)
                .TakeWhile(evArg => _running)
                .Select(evArgs => evArgs.EventArgs.Message);
            _client = new ClientConnection(Guid.NewGuid(), msgStream, SendText);
            _connections?.Invoke(this, new HandlerEventArgs(this));
        }

        private void SendText(byte[] data)
        {
            var str = Encoding.UTF8.GetString(data);
            Send(str);
        }

        /// <summary>
        /// Called when a websocket message is received
        /// </summary>
        /// <param name="message"></param>
        protected override void OnMessage(MessageEventArgs e)
        {
            _messages?.Invoke(this, new GameMessageEventArgs(e.RawData));
        }

        /// <summary>
        /// Called when a connection is closed
        /// </summary>
        protected override void OnError(ErrorEventArgs e)
        {
            _running = false;
            _client.IsRunning = false;
        }

        /// <summary>
        /// Creates an <see cref="IObservable{T}"/> containing all of the information about client connections and messages
        /// </summary>
        public static IObservable<ClientConnection> Connections
        {
            get
            {
                return Observable.FromEventPattern<HandlerEventArgs>(
                    handler => _connections += handler,
                    handler => _connections -= handler)
                    .Select(evArgs => evArgs.EventArgs.Handler)
                    .Select(handler => handler._client);
            }
        }

        private class GameMessageEventArgs: EventArgs
        {
            public GameMessageEventArgs(byte[] message)
            {
                Message = message;
            }
            public byte[] Message;
        }

        private class HandlerEventArgs : EventArgs
        {
            public HandlerEventArgs(GameModelHandler handler)
            {
                Handler = handler;
            }
            public GameModelHandler Handler;
        }

        private static event EventHandler<HandlerEventArgs> _connections;
        private event EventHandler<GameMessageEventArgs> _messages;
        private ClientConnection _client;
        private bool _running;
    }
}