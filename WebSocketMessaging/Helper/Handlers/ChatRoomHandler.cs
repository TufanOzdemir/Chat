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

        /// <summary>
        /// Socket isteği text ise middleware den bu fonksiyon çağırılır. Mesaj atar.
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="result"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            string message = $"{GetSocketModelName(socketModel)}: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAllAsync(message);
        }

        /// <summary>
        /// Yeni katılan kullanıcılar için çalışır.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            await SendMessageToAllAsync($"{GetSocketModelName(socketModel)} is now connected");
        }

        /// <summary>
        /// Çıkış yapan kullanıcılar için çalışır.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketModel = WebSocketConnectionManager.GetSocketModel(socket);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{GetSocketModelName(socketModel)} is now disconnected");
        }

        /// <summary>
        /// Eğer socket ismi varsa ismini yoksa id bilgisini ekrana yazması için kullanılır.
        /// </summary>
        /// <param name="socketModel"></param>
        /// <returns></returns>
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
