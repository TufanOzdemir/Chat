using Model;
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
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            string message = $"{GetSocketModelName(socketModel)}: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAllAsync(message);
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            await SendMessageToAllAsync($"{GetSocketModelName(socketModel)} is now connected");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{GetSocketModelName(socketModel)} is now disconnected");
        }

        private string GetSocketModelName(SocketModel socketModel)
        {
            string result = socketModel.Name;
            if (string.IsNullOrWhiteSpace(socketModel.Name))
            {
                result = socketModel.Id;
            }
            return result;
        }
    }
}
