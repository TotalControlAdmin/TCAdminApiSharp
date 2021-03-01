using Newtonsoft.Json.Linq;
using RestSharp;

namespace TCAdminApiSharp.Querying
{
    public interface IQueryOperation
    {
        string JsonKey { get; set; }
        
        void ModifyRequest(IRestRequest request)
        {

        }
    }
}