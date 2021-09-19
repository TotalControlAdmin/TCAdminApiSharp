using Newtonsoft.Json;
using RestSharp;

namespace TCAdminApiSharp.Entities.API
{
    public class BaseResponse
    {
        // Default to true
        [JsonProperty("Success")] public bool Success { get; private set; } = true;

        [JsonProperty("Message")] public string? Message { get; private set; }

        [JsonIgnore] public IRestResponse RestResponse { get; internal set; }

        internal BaseResponse()
        {
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        [JsonProperty("Result")] public T Result { get; private set; }

        internal BaseResponse()
        {
        }
    }
}