using System.Collections.Generic;
using System.Linq;

namespace GameModel.Server
{
    public class UnassignedLobby
    {
        public UnassignedLobby(ICollection<ClientConnection> players)
        {
            Players = players;
        }

        public void RemoveDead()
        {
            Players = Players.Where(p => p.IsRunning)
                             .ToList();
        }

        public bool IsFull => Players.Count == FULL_SIZE;
        public ICollection<ClientConnection> Players { get; private set; }
        public const int FULL_SIZE = 4;
    }
}