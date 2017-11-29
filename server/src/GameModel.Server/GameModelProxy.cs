using GameModel.Client;
using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;

namespace GameModel.Server
{
    public static class GameModelProxy
    {
        /// <summary>
        /// Processes all of the active lobbies
        /// </summary>
        /// <param name="lobbies"></param>
        public static void ProxyGameModel(this IObservable<IDictionary<PlayerEnum, ClientConnection>> lobbies)
        {
            lobbies.Subscribe((players) =>
            {
                var arby = new LocalArbitrator();
                var terminateGame = new CancellationTokenSource();
                var arbyLock = new object();
                foreach (var kvp in players)
                {
                    var conn = kvp.Value;
                    var msgs = conn.IncomingMessages
                        .ParseMessages();
                    //Listens and performs piece translations
                    msgs.Where(msg => msg is TranslatePieceMessage)
                        .Cast<TranslatePieceMessage>()
                        .Do(translatation =>
                        {
                            lock (arbyLock)
                            {
                                arby.MakeMove(translatation.src, translatation.dest);
                            }
                        })
                        .Subscribe(terminateGame.Token);
                }
                //Send messages back to clients
                Observable.Interval(TimeSpan.FromMilliseconds(200))
                    .SelectMany((i) => MessagesFromArbitrator(arby))
                    .SerializeMessages()
                    .Do(msg =>
                    {
                        foreach (var client in players.Values)
                        {
                            if (client.IsRunning)
                            {
                                client.SendCallback(msg);
                            }
                            else
                            {
                                terminateGame.Cancel();
                            }
                        }
                    })
                    .Subscribe(terminateGame.Token);
            });
        }
        private static IEnumerable<ModelMessage> MessagesFromArbitrator(IArbitrator arby)
        {
            ModelMessage msg;
            while (arby.TryGetLatestMessage(out msg))
            {
                yield return msg;
            }
        }
    }
}