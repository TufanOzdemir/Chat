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

        /// <summary>
        /// Id bilgisi ile socket çekme işlemi yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key.Id == id).Value;
        }

        /// <summary>
        /// Tüm socket bilgilerini döndürür.
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<SocketModel, WebSocket> GetAll()
        {
            return _sockets;
        }

        /// <summary>
        /// Socket bilgisi ile id bilgisi çekilir.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key.Id;
        }

        /// <summary>
        /// Socket bilgisi ile SocketModel bilgisi çekilir.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public SocketModel GetSocketModel(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        /// <summary>
        /// Id bilgisi ile SocketModel bilgisi çekilir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SocketModel GetSocketModelById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key.Id == id).Key;
        }

        /// <summary>
        /// Socket ekler.
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="title">İsim bilgisi</param>
        public void AddSocket(WebSocket socket, string title = "")
        {
            string sId = ConnectionIdFactory();
            while (!_sockets.TryAdd(new SocketModel { Id = sId, Name = title }, socket))
            {
                sId = ConnectionIdFactory();
            }
        }

        /// <summary>
        /// Socket siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Rasgele Guid üretir. Her socket ekleneceğinde unique Id olması amaçlanır.
        /// </summary>
        /// <returns></returns>
        private string ConnectionIdFactory()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
