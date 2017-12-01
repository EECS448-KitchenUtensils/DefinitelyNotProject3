using System.Collections.Generic;
using System.Linq;

namespace GameModel.Server
{
    /// <summary>
    /// Represents a possibly-unfilled lobby that has not had player precendences assigned yet
    /// </summary>
    public class UnassignedLobby
    {
        /// <summary>
        /// Creates a new <see cref="UnassignedLobby"/> from a collection of players
        /// </summary>
        /// <param name="players"><see cref="ICollection{ClientConnection}"/> of players to hold</param>
        public UnassignedLobby(ICollection<ClientConnection> players)
        {
            Players = players;
        }

        /// <summary>
        /// Prunes inactive client connections
        /// </summary>
        public void RemoveDead()
        {
            Players = Players.Where(p => p.IsRunning)
                             .ToList();
        }

        /// <summary>
        /// Checks if the lobby contains enough players to be considered full
        /// </summary>
        public bool IsFull => Players.Count == FULL_SIZE;

        /// <summary>
        /// Players contained within this lobby
        /// </summary>
        public ICollection<ClientConnection> Players { get; private set; }

        /// <summary>
        /// Size of a full lobby
        /// </summary>
        public const int FULL_SIZE = 4;
    }
}