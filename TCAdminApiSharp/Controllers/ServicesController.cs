using System;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TCAdminApiSharp.Entities;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Exceptions.Services;

namespace TCAdminApiSharp.Controllers
{
    public class ServicesController : BaseController
    {
        public ServicesController() : base("api/service")
        {
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
                    throw new ServiceNotFoundException(serviceId, e);
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