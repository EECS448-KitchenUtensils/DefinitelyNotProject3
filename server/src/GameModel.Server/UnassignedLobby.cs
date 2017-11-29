using System.Collections.Generic;

namespace GameModel.Server
{
    public class UnassignedLobby
    {
        public UnassignedLobby(ICollection<ClientConnection> players)
        {
            Players = players;
        }
        public bool IsFull => Players.Count == FULL_SIZE;
        public readonly ICollection<ClientConnection> Players;
        public const int FULL_SIZE = 4;
    }
}