using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Querying;

namespace TCAdminApiSharp.Controllers
{
    public class BaseController
    {
        public readonly TcaClient TcaClient =
            TcaClient.ServiceProvider.GetService<TcaClient>() ?? throw new InvalidOperationException();

        public readonly string BaseResource;
        internal readonly ILogger Logger;

        protected BaseController(string baseResource)
        {
            Logger = Log.ForContext(GetType());
            BaseResource = baseResource;

            if (!baseResource.EndsWith("/")) BaseResource = baseResource + "/";
        }

        public RestRequest GenerateDefaultRequest()
        {
            return new(BaseResource);
        }
        
        public async Task<BaseResponse<T>> AdvancedRequest<T>(string resource, QueryableInfo query, Method method = Method.POST)
        {
            var request = GenerateDefaultRequest();
            request.Method = method;
            request.Resource += resource;
            query.BuildQuery(request);
            return await ExecuteBaseResponseRequest<T>(request);
        }
        
        public async Task<ListResponse<T>> AdvancedListRequest<T>(string resource, QueryableInfo query, Method method = Method.POST)
        {
            var request = GenerateDefaultRequest();
            request.Method = method;
            request.Resource += resource;
            query.BuildQuery(request);
            return await ExecuteListResponseRequest<T>(request);
        }

        public async Task<BaseResponse> ExecuteBaseResponseRequest(RestRequest request)
        {
            var (baseResponse, restResponse) = await ExecuteRequestAsync<BaseResponse>(request);
            baseResponse.RestResponse = restResponse;
            return baseResponse;
        }

        public async Task<BaseResponse<T>> ExecuteBaseResponseRequest<T>(RestRequest request)
        {
            var (item1, item2) = await ExecuteRequestAsync<BaseResponse<T>>(request);
            item1.RestResponse = item2;
            return item1;
        }

        public async Task<ListResponse<T>> ExecuteListResponseRequest<T>(RestRequest request)
        {
            var (baseResponse, restResponse) = await ExecuteRequestAsync<ListResponse<T>>(request);
            baseResponse.RestResponse = restResponse;
            return baseResponse;
        }

        public async Task<Tuple<T, IRestResponse>> ExecuteRequestAsync<T>(RestRequest request)
        {
            Logger.Debug($"Request URL [{request.Method}]: {TcaClient.RestClient.BuildUri(request)}");
            var restResponse = await TcaClient.RestClient.ExecuteAsync(request);
            Logger.Debug("Response Status: " + restResponse.ResponseStatus);
            Logger.Debug("Status Code: " + restResponse.StatusCode);
            Logger.Debug("Parameters:");
            foreach (var requestParameter in request.Parameters)
            {
                Logger.Debug(requestParameter.ToString());
            }
            if (restResponse.ResponseStatus != ResponseStatus.Completed)
            {
                if (!TcaClient.Settings.ThrowOnApiResponseStatusNonComplete)
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                throw new ApiResponseException(restResponse, "Response Status is: " + restResponse.ResponseStatus);
            }

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                if (!TcaClient.Settings.ThrowOnApiStatusCodeNonOk)
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                throw new ApiResponseException(restResponse, $"Status code is: {restResponse.StatusCode}\n{restResponse.Content}");
            }

            var baseResponse =
                JsonConvert.DeserializeObject<BaseResponse<object>>(restResponse
                    .Content); // First deserialize to the base response
            baseResponse.RestResponse = restResponse;
            if (!baseResponse.Success)
            {
                var tType = typeof(T);
                if (tType.GenericTypeArguments.Contains(baseResponse.Result.GetType()))
                    return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content),
                        restResponse);

                if (!TcaClient.Settings.ThrowOnApiSuccessFailure)
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                throw new ApiResponseException(restResponse, "API returned an error");
            }

            return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content), restResponse);
        }
    }
}