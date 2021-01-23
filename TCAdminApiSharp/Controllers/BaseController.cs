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
using TCAdminApiSharp.Helpers;

namespace TCAdminApiSharp.Controllers
{
    public class BaseController
    {
        public readonly TcaClient TcaClient =
            TcaClient.ServiceProvider.GetService<TcaClient>() ?? throw new InvalidOperationException();
        public readonly string BaseResource;
        public readonly ILogger Logger;

        protected BaseController(string baseResource)
        {
            Logger = Log.ForContext(GetType());
            BaseResource = baseResource;

            if (!baseResource.EndsWith("/"))
            {
                BaseResource = baseResource + "/";
            }
        }

        public RestRequest GenerateDefaultRequest()
        {
            return new(BaseResource);
        }
        
        public BaseResponse ExecuteBaseResponseRequest(RestRequest request)
        {
            var response = ExecuteRequest<BaseResponse>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }

        public BaseResponse<T> ExecuteBaseResponseRequest<T>(RestRequest request)
        {
            var response = ExecuteRequest<BaseResponse<T>>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }
        
        public ListResponse<T> ExecuteListResponseRequest<T>(RestRequest request)
        {
            var response = ExecuteRequest<ListResponse<T>>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }
        
        public T ExecuteRequest<T>(RestRequest request)
        {
            return ExecuteRequest<T>(request, out _);
        }

        public T ExecuteRequest<T>(RestRequest request, out IRestResponse restResponse)
        {
            var (t, restResponse2) = ExecuteRequestAsync<T>(request).ConfigureAwait(false).GetAwaiter().GetResult();
            restResponse = restResponse2;
            return t;
        }

        public async Task<Tuple<T, IRestResponse>> ExecuteRequestAsync<T>(RestRequest request)
        {
            Logger.Verbose(JsonConvert.SerializeObject(request, Constants.IgnoreReferenceLoop));
            Logger.Debug($"Request URL [{request.Method}]: {TcaClient.RestClient.BuildUri(request)}");
            var restResponse = await TcaClient.RestClient.ExecuteAsync(request);
            Logger.Verbose(JsonConvert.SerializeObject(restResponse, Constants.IgnoreReferenceLoop));
            Logger.Debug("Response Status: " + restResponse.ResponseStatus);
            Logger.Debug("Status Code: " + restResponse.StatusCode);
            if (restResponse.ResponseStatus != ResponseStatus.Completed)
            {
                if (!TcaClient.Settings.ThrowOnApiResponseStatusNonComplete)
                {
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                }
                throw new ApiResponseException(restResponse, "Response Status is: " + restResponse.ResponseStatus);
            }

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                if (!TcaClient.Settings.ThrowOnApiStatusCodeNonOk)
                {
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                }
                throw new ApiResponseException(restResponse, "Status code is: " + restResponse.StatusCode);
            }

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(restResponse.Content); // First deserialize to the base response
            baseResponse.RestResponse = restResponse;
            if (!baseResponse.Success)
            {
                var tType = typeof(T);
                if (tType.GenericTypeArguments.Contains(baseResponse.Result.GetType()))
                {
                    return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content), restResponse);
                }

                if (!TcaClient.Settings.ThrowOnApiSuccessFailure)
                {
                    return new Tuple<T, IRestResponse>(default!, restResponse);
                }
                throw new ApiResponseException(restResponse, "API returned an error");
            }

            return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content), restResponse);
        }
    }
}