using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Querying;
using TCAdminApiSharp.Querying.Operations;
using TCAdminApiSharp.Querying.Operators;

namespace TCAdminApiSharp.Controllers;

public class ServicesController : BaseController
{
    public ServicesController(TcaClient tcaClient) : base(tcaClient, "api/service")
    {
    }

    public async Task<int> CreateService(ServiceBuilder builder)
    {
        var body = builder.GenerateRequestBody();
        var request = GenerateDefaultRequest("create");
        request.Method = HttpMethod.Post;
        request.Content = new FormUrlEncodedContent(new []{new KeyValuePair<string, string>("createinfo", body)});
        var response = await ExecuteBaseResponseRequest<int>(request);
        return response.Result;
    }

    public async Task<ListResponse<Service>> FindServices(QueryableInfo query)
    {
        var request = GenerateDefaultRequest(HttpMethod.Post, "gameservices");
        query.BuildQuery(request);
        var result = await ExecuteListResponseRequest<Service>(request);
        return result;
    }

    public async Task<Service> GetService(int serviceId)
    {
        var request = GenerateDefaultRequest(serviceId.ToString());
        var result = (await ExecuteBaseResponseRequest<Service>(request)).Result;
        return result;
    }

    public async Task<ListResponse<Service>> GetServices()
    {
        var request = GenerateDefaultRequest("gameservices");
        var result = await ExecuteListResponseRequest<Service>(request);
        return result;
    }

    public Task<ListResponse<Service>> GetServicesByBillingId(string billingId)
    {
        return FindServices(new QueryableInfo(new WhereList("BillingId", ColumnOperator.Equal, billingId)));
    }

    public async Task<ListResponse<Service>> GetServicesByUserId(int userId)
    {
        var request = GenerateDefaultRequest(QueryHelpers.AddQueryString("gameservices", nameof(userId), userId.ToString()));
        var result = await ExecuteListResponseRequest<Service>(request);
        return result;
    }
}