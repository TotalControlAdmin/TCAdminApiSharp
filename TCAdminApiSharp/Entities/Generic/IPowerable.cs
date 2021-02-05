using System.Threading.Tasks;

namespace TCAdminApiSharp.Entities.Generic
{
    public interface IPowerable
    {
        public Task<bool> Start(string reason = "");
        public Task<bool> Restart(string reason = "");
        public Task<bool> Stop(string reason = "");
    }
}