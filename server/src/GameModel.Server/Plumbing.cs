﻿namespace GameModel.Server
{
    public class Plumbing
    {
        public Plumbing(ClientConnection client)
        {
            _client = client;
        }
        /// <summary>
        /// Sets up the message processing pipeline
        /// </summary>
        public void Plumb()
        {
            _client.IncomingMessages
                
        }
        private ClientConnection _client;
    }
}