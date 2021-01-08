using System;
using Newtonsoft.Json;
using RestSharp;

namespace TCAdminApiSharp.Entities.API
{
    public class ErrorResponse : BaseResponse<object>
    {
        internal ErrorResponse(IRestResponse restResponse)
        {
            try
            {
                JsonConvert.PopulateObject(restResponse.Content, this);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't parse API error to object!", e);
            }
            this.RestResponse = restResponse;
        }
    }
}