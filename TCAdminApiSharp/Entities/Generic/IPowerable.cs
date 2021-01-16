using System.Threading.Tasks;

namespace TCAdminApiSharp.Entities.Generic
{
    public interface IPowerable
    {
        public void Start(string reason = "");
        public void Restart(string reason = "");
        public void Stop(string reason = "");
    }
}