using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Generic;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Helpers;
using TCAdminApiSharp.Querying;

namespace TCAdminApiSharp.Controllers;

public class BaseController
{
    public readonly TcaClient TcaClient;
    public readonly string BaseResource;
    internal readonly ILogger Logger;

    protected BaseController(TcaClient tcaClient, string baseResource)
    {
        Logger = Log.ForContext(GetType());
        TcaClient = tcaClient;
        BaseResource = baseResource;

        if (!baseResource.EndsWith("/")) BaseResource = baseResource + "/";
    }
        
    public HttpRequestMessage GenerateDefaultRequest()
    {
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.RequestUri = Append(new Uri(BaseResource, UriKind.RelativeOrAbsolute));
        return httpRequestMessage;
    }

    public HttpRequestMessage GenerateDefaultRequest(params string[] paths)
    {
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.RequestUri = Append(new Uri(BaseResource, UriKind.RelativeOrAbsolute), paths);
        return httpRequestMessage;
    }
    
    public HttpRequestMessage GenerateDefaultRequest(HttpMethod httpMethod, params string[] paths)
    {
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.RequestUri = Append(new Uri(BaseResource, UriKind.RelativeOrAbsolute), paths);
        httpRequestMessage.Method = httpMethod;
        return httpRequestMessage;
    }
        
    public async Task<BaseResponse<T>> AdvancedRequest<T>(string resource, QueryableInfo query, HttpMethod method)
    {
        var request = GenerateDefaultRequest(resource);
        request.Method = method;
        query.BuildQuery(request);
        return await ExecuteBaseResponseRequest<T>(request);
    }
        
    public async Task<ListResponse<T>> AdvancedListRequest<T>(string resource, QueryableInfo query, HttpMethod method)
    {
        var request = GenerateDefaultRequest(resource);
        request.Method = method;
        query.BuildQuery(request);
        return await ExecuteListResponseRequest<T>(request);
    }

    public async Task<BaseResponse> ExecuteBaseResponseRequest(HttpRequestMessage requestMessage)
    {
        var (baseResponse, httpResponse) = await ExecuteRequestAsync<BaseResponse>(requestMessage);
        baseResponse.RequestMessage = requestMessage;
        baseResponse.ResponseMessage = httpResponse;
        return baseResponse;
    }

    public async Task<BaseResponse<T>> ExecuteBaseResponseRequest<T>(HttpRequestMessage requestMessage)
    {
        var (baseResponse, httpResponse) = await ExecuteRequestAsync<BaseResponse<T>>(requestMessage);
        baseResponse.RequestMessage = requestMessage;
        baseResponse.ResponseMessage = httpResponse;
        return baseResponse;
    }

    public async Task<ListResponse<T>> ExecuteListResponseRequest<T>(HttpRequestMessage requestMessage)
    {
        var (baseResponse, restResponse) = await ExecuteRequestAsync<ListResponse<T>>(requestMessage);
        return baseResponse;
    }

    public async Task<Tuple<T, HttpResponseMessage>> ExecuteRequestAsync<T>(HttpRequestMessage request)
    {
        Logger.Debug("Request: {Request}", request.ToString());
        var httpResponseMessage = await TcaClient.HttpClient.SendAsync(request);
        Logger.Debug("Response: {Response}", httpResponseMessage.ToString());
        var strResponse = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            if (!TcaClient.Settings.ThrowOnApiResponseStatusNonComplete)
                return new Tuple<T, HttpResponseMessage>(default!, httpResponseMessage);

            if (!string.IsNullOrEmpty(strResponse))
            {
                var exceptionResponse = JsonConvert.DeserializeObject<BaseResponse<Exception>>(strResponse);
                throw new ApiResponseException(httpResponseMessage, exceptionResponse);
            }
            
            throw new ApiResponseException(httpResponseMessage, "Response Status Code is: " + httpResponseMessage.StatusCode);
        }

        var response =
            JsonConvert.DeserializeObject<T>(strResponse, Constants.IgnoreDefaultValues);
        if (response != null) ApplyObjectBaseTCAClient(response, true);

        return new Tuple<T, HttpResponseMessage>(response, httpResponseMessage);;
    }

    public void ApplyObjectBaseTCAClient(object obj, bool recursive)
    {
        var type = obj.GetType();
        // if (type.IsSubclassOf(typeof(ITCAdminClientCompatible)))
        if (typeof(ITCAdminClientCompatible).IsAssignableFrom(type))
        {
            type.GetProperty(nameof(ITCAdminClientCompatible.TcaClient))?.SetValue(obj, TcaClient);
        }

        if (recursive)
        {
            if (type.IsSubclassOf(typeof(BaseResponse)))
            {
                var value = type.GetProperty("Result")?.GetValue(obj);
                if (value != null)
                {
                    ApplyObjectBaseTCAClient(value, recursive);
                }
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var enumerable = (IEnumerable<object>)obj;
                foreach (var o in enumerable)
                {
                    ApplyObjectBaseTCAClient(o, recursive);
                }
            }
        }
    }
        
    public static Uri Append(Uri uri, params string[] paths)
    {
        return new Uri(paths.Aggregate(uri.ToString(), (current, path) =>
            $"{current.TrimEnd('/')}/{path.TrimStart('/')}"), UriKind.RelativeOrAbsolute);
    }
}