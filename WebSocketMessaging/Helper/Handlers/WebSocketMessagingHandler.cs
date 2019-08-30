using Interface.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Handlers
{
    public class WebSocketMessagingHandler : SocketHandler
    {

        public WebSocketMessagingHandler(ISocketManager socketManager) : base(socketManager) { }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = _socketManager.GetConnectionId(socket);
            var message = $"{socketId}: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessage(socketId, message);
        }
    }
}
