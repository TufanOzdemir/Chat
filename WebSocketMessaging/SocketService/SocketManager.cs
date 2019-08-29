using Interface.ServiceInterfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketService
{
    public class SocketManager : ISocketManager
    {
        public ConcurrentDictionary<string, WebSocket> _socketDictionary { get; set; }

        public SocketManager()
        {
            _socketDictionary = new ConcurrentDictionary<string, WebSocket>();
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnection()
        {
            return _socketDictionary;
        }

        public WebSocket GetConnectionById(string id)
        {
            _socketDictionary.TryGetValue(id, out var result);
            return result;
        }

        public string GetConnectionId(WebSocket webSocket)
        {
            return _socketDictionary.FirstOrDefault(c => c.Value == webSocket).Key;
        }

        public void AddConnection(WebSocket model)
        {
            _socketDictionary.TryAdd(ConnectionIdFactory(), model);
        }

        public async Task DeleteConnection(string id)
        {
            _socketDictionary.TryRemove(id, out var result);
            await result.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket Kapatıldı!", CancellationToken.None);
        }

        public string ConnectionIdFactory()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}