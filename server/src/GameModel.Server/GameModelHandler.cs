using Microsoft.Web.WebSockets;

namespace GameModel.Server
{
    /// <summary>
    /// Handles a connection with a client GameModel over a websocket
    /// </summary>
    public class GameModelHandler: WebSocketHandler
    {
        public override void OnOpen()
        {
            base.OnOpen();
        }

        public override void OnMessage(string message)
        {
            base.OnMessage(message);
        }

        public override void OnClose()
        {
            base.OnClose();
        }
    }
}