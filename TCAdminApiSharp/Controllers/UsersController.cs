using System.Net;
using System.Threading.Tasks;
using RestSharp;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.User;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Querying;

namespace TCAdminApiSharp.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController() : base("api/user")
        {
        }

        public async Task<User> GetUser(int userId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += userId;
                var response = await ExecuteBaseResponseRequest<User>(request);
                return response.Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException(typeof(User), e, new[] {userId});
                throw;
            }
        }

        public async Task<User> GetMe()
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += "me";
                var response = await ExecuteBaseResponseRequest<User>(request);
                return response.Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException(typeof(User), e, new[] {TcaClient.GetTokenUserId()});
                throw;
            }
        }

        public async Task<ListResponse<User>> FindUsers(QueryableInfo query)
        {
            var request = GenerateDefaultRequest();
            // Logger.Debug(query.BuildQuery(request));
            request.Method = Method.POST;
            request.Resource += $"users/{TcaClient.GetTokenUserId()}";
            // request.AddParameter("queryInfo", query.BuildQuery(request), ParameterType.GetOrPost);
            query.BuildQuery(request);
            return await ExecuteListResponseRequest<User>(request);
        }

        public async Task<ListResponse<User>> GetMyUsers()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "myusers";
            return await ExecuteListResponseRequest<User>(request);
        }

        public async Task<ListResponse<User>> GetUsers()
        {
            var request = GenerateDefaultRequest();
            request.Resource += $"users/{TcaClient.GetTokenUserId()}";
            return await ExecuteListResponseRequest<User>(request);
        }
    }
}