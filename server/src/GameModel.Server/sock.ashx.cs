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
        /// <summary>
        /// Processes an HTTP request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new GameModelHandler().ProcessWebSocketRequestAsync);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Write("Expected a Websocket Upgrade\r\n");
                context.Response.Close();
            }
        }

        /// <summary>
        /// This Controller is reusable between requests
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}