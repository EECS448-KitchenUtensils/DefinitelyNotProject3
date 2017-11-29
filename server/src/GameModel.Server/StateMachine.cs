using GameModel.Messages;
using System;
using System.Diagnostics;

namespace GameModel.Server
{
    /// <summary>
    /// Encapsulates the state of an active connection.
    /// </summary>
    public class StateMachine : IDisposable
    {
        public StateMachine(string clientId)
        {
            _clientId = clientId;
        }

        public enum ConnectionState
        {
            /// <summary>
            /// When a client has just connected, but has yet to be queued
            /// </summary>
            Connected,
            /// <summary>
            /// When a client is queued for joining a lobby
            /// </summary>
            Queued,
            /// <summary>
            /// When a lobby is about to start a game
            /// </summary>
            PreGame,
            /// <summary>
            /// When a client and lobby are in a game
            /// </summary>
            InGame
        }

        /// <summary>
        /// The current state of the connection
        /// </summary>
        public ConnectionState State { get; private set; }
        
        /// <summary>
        /// Ingests a message from the network
        /// </summary>
        /// <param name="msg">A message to be processed</param>
        public void IngestMessage(ModelMessage msg)
        {
            switch (State)
            {
                case ConnectionState.Connected:
                    IngestMessageConnected(msg);
                    return;
                case ConnectionState.Queued:
                    IngestMessageQueued(msg);
                    return;
                case ConnectionState.PreGame:
                    IngestMessagePreGame(msg);
                    return;
                case ConnectionState.InGame:
                    IngestMessageInGame(msg);
                    return;
            }
        }

        private void IngestMessageInGame(ModelMessage msg)
        {
            switch (msg)
            {
                case TranslatePieceMessage translate:
                    //TODO: Tell the model proxy about this
                    return;
                default:
                    HandleInvalidMessage(msg);
                    return;
            }
        }
        
        private void IngestMessagePreGame(ModelMessage msg)
        {
            switch (msg)
            {
                default:
                    HandleInvalidMessage(msg);
                    return;
            }
        }

        private void IngestMessageQueued(ModelMessage msg)
        {
            switch (msg)
            {
                default:
                    HandleInvalidMessage(msg);
                    return;
            }
        }

        private void IngestMessageConnected(ModelMessage msg)
        {
            switch (msg)
            {
                default:
                    HandleInvalidMessage(msg);
                    return;
            }
        }
        
        private void HandleInvalidMessage(ModelMessage msg)
        {
            Debug.WriteLine($"Unexpected message. Client: {_clientId} State: {State} Message: {msg}");
        }

        /// <summary>
        /// Called to abruptly stop the <see cref="StateMachine"/> processing.
        /// </summary>
        public void Dispose()
        {
            //nop
        }

        private string _clientId;
    }
}