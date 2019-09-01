using Interface.ServiceInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketService
{
    public class WebSocketConnectionManager : ISocketManager
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public void AddSocket(WebSocket socket)
        {
            string sId = ConnectionIdFactory();
            while (!_sockets.TryAdd(sId, socket))
            {
                sId = ConnectionIdFactory();
            }
        }

        public async Task RemoveSocket(string id)
        {
            try
            {
                _sockets.TryRemove(id, out var socket);
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
