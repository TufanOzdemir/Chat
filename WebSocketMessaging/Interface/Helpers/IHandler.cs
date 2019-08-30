using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Helpers
{
    public interface IHandler<Tkey, Tobj>
    {
        Task OnConnected(Tobj model);
        Task OnDisconnected(Tobj model);
        Task OnDisconnected(Tkey id);
        Task SendMessage(Tobj model, string message);
        Task SendMessage(Tkey id, string message);
    }
}
