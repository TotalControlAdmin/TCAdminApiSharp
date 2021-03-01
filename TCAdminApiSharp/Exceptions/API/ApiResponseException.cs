using System;
using RestSharp;
using TCAdminApiSharp.Entities;
using TCAdminApiSharp.Entities.API;

namespace TCAdminApiSharp.Exceptions.API
{
    public class ApiResponseException : Exception
    {
        public readonly IRestResponse RestResponse;
        public readonly ErrorResponse ErrorResponse;

        internal ApiResponseException()
        {
        }

        internal ApiResponseException(IRestResponse restResponse) : this()
        {
            RestResponse = restResponse;
            ErrorResponse = new ErrorResponse(restResponse);
        }

        internal ApiResponseException(IRestResponse restResponse, string message) : base(message)
        {
            RestResponse = restResponse;
            ErrorResponse = new ErrorResponse(restResponse);
        }
    }
}