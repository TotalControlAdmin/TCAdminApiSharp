using System;
using RestSharp;

namespace TCAdminApiSharp.Exceptions.API
{
    public class ApiRequestException : Exception
    {
        public readonly RestRequest RestRequest;

        internal ApiRequestException(RestRequest restRequest)
        {
            RestRequest = restRequest;
        }

        internal ApiRequestException(RestRequest restRequest, string? message) : base(message)
        {
            RestRequest = restRequest;
        }

        internal ApiRequestException(RestRequest restRequest, string? message, Exception? innerException) : base(
            message, innerException)
        {
            RestRequest = restRequest;
        }

        internal ApiRequestException(RestRequest restRequest, Exception? innerException) : base(null, innerException)
        {
            RestRequest = restRequest;
        }
    }
}