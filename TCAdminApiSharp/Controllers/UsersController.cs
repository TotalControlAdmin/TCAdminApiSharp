using System.Net.Http;
using System.Threading.Tasks;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.User;
using TCAdminApiSharp.Querying;

namespace TCAdminApiSharp.Controllers;

public class UsersController : BaseController
{
    public UsersController(TcaClient tcaClient) : base(tcaClient, "api/user")
    {
    }

    public async Task<User> GetUser(int userId)
    {
        var request = GenerateDefaultRequest(userId.ToString());
        var response = await ExecuteBaseResponseRequest<User>(request);
        return response.Result;
    }

    public async Task<User> GetMe()
    {
        var request = GenerateDefaultRequest("me");
        var response = await ExecuteBaseResponseRequest<User>(request);
        return response.Result;
    }

    public async Task<ListResponse<User>> FindUsers(QueryableInfo query)
    {
        var tokenUserId = await TcaClient.GetTokenUserId();
        var request = GenerateDefaultRequest(HttpMethod.Post, "myusers", tokenUserId.ToString());
        request.Method = HttpMethod.Post;
        query.BuildQuery(request);
        return await ExecuteListResponseRequest<User>(request);
    }

    public async Task<ListResponse<User>> GetMyUsers()
    {
        var request = GenerateDefaultRequest("myusers");
        return await ExecuteListResponseRequest<User>(request);
    }

    public async Task<ListResponse<User>> GetUsers()
    {
        var request = GenerateDefaultRequest("users", TcaClient.GetTokenUserId().ToString());
        return await ExecuteListResponseRequest<User>(request);
    }
}