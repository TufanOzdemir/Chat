using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Interface.ServiceInterfaces
{
    public interface ISocketManager : IConnectionManager<string, WebSocket>
    {
    }
}
