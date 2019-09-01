using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Interface.ServiceInterfaces
{
    public interface IConnectionManager<Tkey, Tobj>
    {
        ConcurrentDictionary<Tkey, Tobj> GetAll();
        Tkey GetId(Tobj model);
        Tobj GetSocketById(Tkey id);
        void AddSocket(Tobj model);
        Task RemoveSocket(Tkey id);
    }
}