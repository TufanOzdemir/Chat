using Model;
using System.Net.WebSockets;

namespace Interface.ServiceInterfaces
{
    public interface ISocketManager : IConnectionManager<SocketModel, WebSocket>
    {
    }
}
