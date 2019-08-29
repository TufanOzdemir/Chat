using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Interface.ServiceInterfaces
{
    public interface IConnectionManager<Tkey, Tobj>
    {
        ConcurrentDictionary<Tkey, Tobj> GetAllConnection();
        Tkey GetConnectionId(Tobj model);
        Tobj GetConnectionById(Tkey id);
        void AddConnection(Tobj model);
        Task DeleteConnection(Tkey id);
        Tkey ConnectionIdFactory();
    }
}