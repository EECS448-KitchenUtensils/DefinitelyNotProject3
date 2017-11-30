using System;
using System.Threading.Tasks;

namespace GameModel.Server
{
    /// <summary>
    /// Immutable data type for tracking client connections
    /// </summary>
    public struct ClientConnection
    {
        /// <summary>
        /// Initializes a <see cref="ClientConnection"/> record
        /// </summary>
        /// <param name="clientId">An identifier unique to this client</param>
        /// <param name="messages">A stream of unparsed messages</param>
        /// <param name="sendCallback">A callback for sending messages back to the client</param>
        public ClientConnection(Guid clientId, IObservable<byte[]> messages, Action<byte[]> sendCallback)
        {
            ClientId = clientId;
            IncomingMessages = messages;
            SendCallback = sendCallback;
            _isRunning = true;
        }
        /// <summary>
        /// An identifier unique to this client
        /// </summary>
        public Guid ClientId;

        public bool IsRunning => _isRunning;

        public void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// A stream of unparsed incoming messages
        /// </summary>
        public IObservable<byte[]> IncomingMessages;

        /// <summary>
        /// A method to call to send a message back to a client
        /// </summary>
        public Action<byte[]> SendCallback;
        private bool _isRunning;
    }
}