using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameModel.Server
{
    /// <summary>
    /// Summary description for WebSocketHandler
    /// </summary>
    public class WebSocketHttpHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new GameModelHandler());
            }
            else
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}