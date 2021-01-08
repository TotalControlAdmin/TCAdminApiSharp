using System;
using RestSharp;

namespace TCAdminApiSharp.Exceptions.API
{
    public class ApiRequestException : Exception
    {
        public readonly RestRequest RestRequest;
        
        internal ApiRequestException(RestRequest restRequest)
        {
            this.RestRequest = restRequest;
        }

        internal ApiRequestException(RestRequest restRequest, string? message) : base(message)
        {
            this.RestRequest = restRequest;
        }

        internal ApiRequestException(RestRequest restRequest, string? message, Exception? innerException) : base(message, innerException)
        {
            this.RestRequest = restRequest;
        }
        
        internal ApiRequestException(RestRequest restRequest, Exception? innerException) : base(null, innerException)
        {
            this.RestRequest = restRequest;
        }
    }
}