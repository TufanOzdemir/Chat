using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Helpers
{
    public interface IHandler<Tkey, Tobj>
    {
        Task OnConnected(Tobj model);
        Task OnConnected(Tobj model, string title);
        Task OnDisconnected(Tobj model);
        Task SendMessageAsync(Tobj model, string message);
        Task SendMessageAsync(Tkey id, string message);
    }
}
