using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace GameModel.Server
{
    public class WebsocketHandler: WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();
        }
    }
}
