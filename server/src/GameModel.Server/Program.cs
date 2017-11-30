using System;
using WebSocketSharp.Server;

namespace GameModel.Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            var server = new WebSocketServer(1337);
            server.AddWebSocketService<GameModelHandler>("/");
            server.Start();
            Plumb();
            Console.ReadKey(true);
            server.Stop();
        }
        private static void Plumb()
        {
            GameModelHandler
                .Connections
                .MakeMatches()
                .AssignPlayerSlots()
                .ProxyGameModel();
        }
    }
}
