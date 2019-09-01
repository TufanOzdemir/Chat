using Interface.ServiceInterfaces;
using Model;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketService
{
    public class WebSocketConnectionManager : ISocketManager
    {
        private ConcurrentDictionary<SocketModel, WebSocket> _sockets = new ConcurrentDictionary<SocketModel, WebSocket>();
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key.Id == id).Value;
        }

        public ConcurrentDictionary<SocketModel, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key.Id;
        }

        public SocketModel GetSocketModel(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public SocketModel GetSocketModelById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key.Id == id).Key;
        }

        public void AddSocket(WebSocket socket, string title = "")
        {
            string sId = ConnectionIdFactory();
            while (!_sockets.TryAdd(new SocketModel { Id = sId, Name = title }, socket))
            {
                sId = ConnectionIdFactory();
            }
        }

        public async Task RemoveSocket(string id)
        {
            try
            {
                var model = GetSocketModelById(id);
                _sockets.TryRemove(model, out var socket);
                await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch (Exception)
            {
            }
        }

        private string ConnectionIdFactory()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
