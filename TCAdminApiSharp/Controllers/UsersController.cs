using System;
using System.Linq.Expressions;
using System.Net;
using Newtonsoft.Json;
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
        
        public User GetUser(int userId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += userId;
                return ExecuteBaseResponseRequest<User>(request).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(typeof(User), e, new []{userId});
                }
                throw;
            }
        }
        
        public User GetMe()
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += "me";
                return ExecuteBaseResponseRequest<User>(request).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(typeof(User), e, new []{TcaClient.GetTokenUserId()});
                }
                throw;
            }
        }

        public ListResponse<User> FindUsers(QueryableInfo query)
        {
            var request = GenerateDefaultRequest();
            Logger.Debug(query.BuildQuery());
            request.Method = Method.POST;
            request.Resource += $"users/{TcaClient.GetTokenUserId()}";
            request.AddParameter("queryInfo", query.BuildQuery(), ParameterType.GetOrPost);
            return ExecuteListResponseRequest<User>(request);
        }
        
        public ListResponse<User> GetMyUsers()
        {
            var request = GenerateDefaultRequest();
            request.Resource += "myusers";
            return ExecuteListResponseRequest<User>(request);
        }
        
        public ListResponse<User> GetUsers()
        {
            var request = GenerateDefaultRequest();
            request.Resource += $"users/{TcaClient.GetTokenUserId()}";
            return ExecuteListResponseRequest<User>(request);
        }
    }
}