using System;
using System.Net.Http;
using Newtonsoft.Json;
using TCAdminApiSharp.Entities.API;

namespace TCAdminApiSharp.Exceptions.API;

public class ApiResponseException : Exception
{
    public readonly HttpResponseMessage HttpResponseMessage;
    public readonly BaseResponse<Exception> ExceptionResponse;

    internal ApiResponseException()
    {
    }

    internal ApiResponseException(HttpResponseMessage httpResponseMessage) : this()
    {
        this.HttpResponseMessage = httpResponseMessage;
        var readAsStringAsync = HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        ExceptionResponse = JsonConvert.DeserializeObject<BaseResponse<Exception>>(readAsStringAsync);
    }

    internal ApiResponseException(HttpResponseMessage httpResponseMessage, string message) : base(message)
    {
        this.HttpResponseMessage = httpResponseMessage;
        var readAsStringAsync = HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        ExceptionResponse = JsonConvert.DeserializeObject<BaseResponse<Exception>>(readAsStringAsync);
    }
    
    internal ApiResponseException(HttpResponseMessage httpResponseMessage, BaseResponse<Exception> exceptionResponse) : base(exceptionResponse.Message)
    {
        this.HttpResponseMessage = httpResponseMessage;
        ExceptionResponse = exceptionResponse;
    }
}