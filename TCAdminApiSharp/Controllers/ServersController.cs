using System.Net;
using System.Threading.Tasks;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Server;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;

namespace TCAdminApiSharp.Controllers
{
    public class ServersController : BaseController
    {
        public ServersController() : base("api/server")
        {
        }

        // public int CreateService(ServiceBuilder builder)
        // {
        //     var body = builder.GenerateRequestBody();
        //     var request = GenerateDefaultRequest();
        //     request.Resource += "create";
        //     request.Method = Method.POST;
        //     request.AddParameter("createinfo", body, ParameterType.GetOrPost);
        //     return ExecuteBaseResponseRequest<int>(request).Result;
        // }

        public async Task<Server> GetServer(int serverId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += serverId;
                return (await ExecuteBaseResponseRequest<Server>(request)).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException(typeof(Service), e, new[] {serverId});

                throw;
            }
        }

        public Task<ListResponse<Server>> GetServers()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "servers";
            return ExecuteListResponseRequest<Server>(request);
        }
    }
}