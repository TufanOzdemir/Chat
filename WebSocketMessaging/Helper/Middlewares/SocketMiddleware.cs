using Helper.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper.Middlewares
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private SocketHandler _handler { get; set; }

        public SocketMiddleware(RequestDelegate next, SocketHandler handler)
        {
            _handler = handler;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await _handler.OnConnected(socket);
            await Receive(socket, async (result, buffer) =>
            {
                switch (result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await _handler.OnDisconnected(socket);
                        break;
                    case WebSocketMessageType.Text:
                        await _handler.Receive(socket, result, buffer);
                        break;
                    default:
                        break;
                }
            });
        }

        private async Task Receive(WebSocket webSocket, Action<WebSocketReceiveResult, byte[]> messageToHandle)
        {
            var buffer = new byte[1024 * 4];
            if (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                messageToHandle(result, buffer);
            }
        }
    }
}
