using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Web;

namespace GameModel.Server
{
    public static class MatchMaking
    {
        public static IObservable<UnassignedLobby> MakeMatches(this IObservable<ClientConnection> unmatchedConnections)
        {
            var accumulator = new UnassignedLobby(Array.Empty<ClientConnection>());
            return unmatchedConnections
            .Synchronize()
            .Select(connection =>
            {
                var activePlayers = accumulator.Players
                           .Where(conn => conn.IsRunning)
                           .ToList();
                activePlayers.Add(connection);
                if (accumulator.IsFull)
                {
                    var oldAccumulator = accumulator;
                    accumulator = new UnassignedLobby(Array.Empty<ClientConnection>());
                    return oldAccumulator;
                }
                else
                {
                    return null;
                }
            })
            .Where(lobby => lobby != null);
        }
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
                return assignedLobby;
            });
        }
    }
}