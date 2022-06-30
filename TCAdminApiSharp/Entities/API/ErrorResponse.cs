using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.API;

public class ErrorResponse : BaseResponse<object>
{
    internal ErrorResponse(HttpResponseMessage responseMessage)
    {
        try
        {
            JsonConvert.PopulateObject(responseMessage.Content.ReadAsStringAsync().Result, this);
        }
        catch (Exception e)
        {
            throw new Exception("Couldn't parse API error to object!", e);
        }

        ResponseMessage = responseMessage;
    }
}