using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Interface.ServiceInterfaces
{
    public interface IConnectionManager<Tkey, Tobj>
    {
        ConcurrentDictionary<Tkey, Tobj> GetAll();
        string GetId(Tobj model);
        Tkey GetSocketModelById(string id);
        Tobj GetSocketById(string id);
        void AddSocket(Tobj model, string title);
        Task RemoveSocket(string id);
    }
}