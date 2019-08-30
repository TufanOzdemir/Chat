using Interface.Helpers;
using Interface.ServiceInterfaces;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper.Handlers
{
    public abstract class SocketHandler : IHandler<string, WebSocket>
    {
        public readonly ISocketManager _socketManager;

        public SocketHandler(ISocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        public virtual async Task OnConnected(WebSocket webSocket)
        {
            await Task.Run(() => { _socketManager.AddConnection(webSocket); });
        }

        public virtual async Task OnDisconnected(WebSocket webSocket)
        {
            await _socketManager.DeleteConnection(_socketManager.GetConnectionId(webSocket));
        }

        public virtual async Task OnDisconnected(string id)
        {
            await _socketManager.DeleteConnection(id);
        }

        public async Task SendMessage(WebSocket webSocket, string message)
        {
            if (webSocket.State != WebSocketState.Open)
            {
                return;
            }
            await webSocket.SendAsync(
                new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        public async Task SendMessage(string id, string message)
        {
            await SendMessage(_socketManager.GetConnectionById(id), message);
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}