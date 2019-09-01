using SocketService;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Handlers
{
    public class ChatRoomHandler : WebSocketHandler
    {
        public ChatRoomHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {

        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            string message = $"{socketId}: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAllAsync(message);
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{socketId} is now disconnected");
        }
    }
}
