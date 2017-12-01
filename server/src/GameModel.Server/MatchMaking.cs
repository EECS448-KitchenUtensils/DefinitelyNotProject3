using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace GameModel.Server
{
    /// <summary>
    /// Contains methods to make lobbies out of connecting clients
    /// </summary>
    public static class MatchMaking
    {
        /// <summary>
        /// Makes lobbies out of incoming connections
        /// </summary>
        /// <param name="unmatchedConnections">Incoming connections that have yet to be assigned to a lobby</param>
        /// <returns>A stream of filled lobbies that have not yet had player precendences assigned</returns>
        public static IObservable<UnassignedLobby> MakeMatches(this IObservable<ClientConnection> unmatchedConnections)
        {
            var accumulator = new UnassignedLobby(new List<ClientConnection>());
            return unmatchedConnections
            .Synchronize()
            .Select(connection =>
            {
                Debug.WriteLine($"Adding new player {connection.ClientId}");
                accumulator.Players.Add(connection);
                accumulator.RemoveDead();
                if (accumulator.IsFull)
                {
                    var oldAccumulator = accumulator;
                    accumulator = new UnassignedLobby(new List<ClientConnection>());
                    Debug.WriteLine($"Filled a lobby");
                    return oldAccumulator;
                }
                else
                {
                    return null;
                }
            })
            .Where(lobby => lobby != null);
        }

        /// <summary>
        /// Assigns precendence values to each player in a newly-filled lobby
        /// </summary>
        /// <param name="lobbies">A stream of new lobbies</param>
        /// <returns>A stream of assigned lobbies</returns>
        public static IObservable<IDictionary<PlayerEnum, ClientConnection>> AssignPlayerSlots(this IObservable<UnassignedLobby> lobbies)
        {
            return lobbies.Select(lobby =>
            {
                var assignedLobby = new Dictionary<PlayerEnum, ClientConnection>();
                var counter = PlayerEnum.PLAYER_1;
                foreach (var player in lobby.Players)
                {
                    assignedLobby[counter] = player;
                    counter++;
                }
                Debug.WriteLine("Assigned player orders to lobby");
                return assignedLobby;
            });
        }
    }
}