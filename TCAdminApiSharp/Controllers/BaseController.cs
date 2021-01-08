﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using TCAdminApiSharp.Entities;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Exceptions.API;

namespace TCAdminApiSharp.Controllers
{
    public class BaseController
    {
        public readonly TcaClient TcaClient =
            TcaClient.ServiceProvider.GetService<TcaClient>() ?? throw new InvalidOperationException();
        public readonly string BaseResource;

        protected BaseController(string baseResource)
        {
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
        
        internal BaseResponse ExecuteBaseResponseRequest(RestRequest request)
        {
            var response = ExecuteRequest<BaseResponse>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }

        internal BaseResponse<T> ExecuteBaseResponseRequest<T>(RestRequest request)
        {
            var response = ExecuteRequest<BaseResponse<T>>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }
        
        internal ListResponse<T> ExecuteListResponseRequest<T>(RestRequest request)
        {
            var response = ExecuteRequest<ListResponse<T>>(request, out var restResponse);
            response.RestResponse = restResponse;
            return response;
        }
        
        internal T ExecuteRequest<T>(RestRequest request)
        {
            return ExecuteRequest<T>(request, out _);
        }

        internal T ExecuteRequest<T>(RestRequest request, out IRestResponse restResponse)
        {
            var (t, restResponse2) = ExecuteRequestAsync<T>(request).ConfigureAwait(false).GetAwaiter().GetResult();
            restResponse = restResponse2;
            return t;
        }

        internal async Task<Tuple<T, IRestResponse>> ExecuteRequestAsync<T>(RestRequest request)
        {
            var restResponse = await TcaClient.RestClient.ExecuteAsync(request);
            if (restResponse.ResponseStatus != ResponseStatus.Completed)
            {
                throw new ApiResponseException(restResponse, "Response Status is: " + restResponse.ResponseStatus);
            }

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiResponseException(restResponse, "Status code is: " + restResponse.StatusCode);
            }

            // Console.WriteLine(_tcaClient.RestClient.BuildUri(request).ToString());
            // Console.WriteLine(restResponse.Content);
            var baseResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(restResponse.Content);
            baseResponse.RestResponse = restResponse;
            if (!baseResponse.Success)
            {
                var tType = typeof(T);
                if (tType.GenericTypeArguments.Contains(baseResponse.Result.GetType()))
                {
                    return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content), restResponse);
                }

                throw new ApiResponseException(restResponse, "API returned an error");
            }

            return new Tuple<T, IRestResponse>(JsonConvert.DeserializeObject<T>(restResponse.Content), restResponse);
        }
    }
}