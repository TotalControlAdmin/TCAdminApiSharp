using System;
using System.Net;
using RestSharp;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;

namespace TCAdminApiSharp.Controllers
{
    public class ServicesController : BaseController
    {
        public ServicesController() : base("api/service")
        {
        }

        public int CreateService(ServiceBuilder builder)
        {
            var body = builder.GenerateRequestBody();
            var request = GenerateDefaultRequest();
            request.Resource += "create";
            request.Method = Method.POST;
            request.AddParameter("createinfo", body, ParameterType.GetOrPost);
            return ExecuteBaseResponseRequest<int>(request).Result;
        }
        
        public Service GetService(int serviceId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += serviceId;
                return ExecuteBaseResponseRequest<Service>(request).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(typeof(Service), e, new []{serviceId});
                }

                throw;
            }
        }
        
        public ListResponse<Service> GetServices()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "gameservices";
            return ExecuteListResponseRequest<Service>(request);
        }
    }
}