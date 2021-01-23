using System;
using System.Net;
using RestSharp;
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
        
        public Server GetServer(int serverId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += serverId;
                return ExecuteBaseResponseRequest<Server>(request).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(typeof(Service), e, new []{serverId});
                }

                throw;
            }
        }
        
        public ListResponse<Server> GetServices()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "servers";
            return ExecuteListResponseRequest<Server>(request);
        }
    }
}