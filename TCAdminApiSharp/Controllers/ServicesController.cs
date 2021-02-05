using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Entities.User;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Querying;
using TCAdminApiSharp.Querying.Operations;
using TCAdminApiSharp.Querying.Operators;

namespace TCAdminApiSharp.Controllers
{
    public class ServicesController : BaseController
    {
        public ServicesController() : base("api/service")
        {
        }

        public async Task<int> CreateService(ServiceBuilder builder)
        {
            var body = builder.GenerateRequestBody();
            var request = GenerateDefaultRequest();
            request.Resource += "create";
            request.Method = Method.POST;
            request.AddParameter("createinfo", body, ParameterType.GetOrPost);
            var response = await ExecuteBaseResponseRequest<int>(request);
            return response.Result;
        }

        public async Task<ListResponse<Service>> FindServices(QueryableInfo query)
        {
            var request = GenerateDefaultRequest();
            Logger.Debug(query.BuildQuery());
            request.Method = Method.POST;
            request.Resource += "gameservices";
            request.AddParameter("queryInfo", query.BuildQuery(), ParameterType.GetOrPost);
            return await ExecuteListResponseRequest<Service>(request);
        }

        public async Task<Service> GetService(int serviceId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += serviceId;
                return (await ExecuteBaseResponseRequest<Service>(request)).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(typeof(Service), e, new[] {serviceId});
                }

                throw;
            }
        }

        public async Task<ListResponse<Service>> GetServices()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "gameservices";
            return await ExecuteListResponseRequest<Service>(request);
        }

        public Task<ListResponse<Service>> GetServicesByBillingId(string billingId)
        {
            return FindServices(new QueryableInfo(new WhereList("BillingId", ColumnOperator.Equal, billingId)));
        }

        public Task<ListResponse<Service>> GetServicesByUserId(int userId)
        {
            return FindServices(new QueryableInfo(new WhereList("UserId", ColumnOperator.Equal, userId)));
        }
    }
}