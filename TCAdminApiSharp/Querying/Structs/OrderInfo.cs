using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TCAdminApiSharp.Querying.Operators;

namespace TCAdminApiSharp.Querying.Structs
{
    public struct OrderInfo
    {
        [JsonProperty("Field")] public string Column;

        [JsonProperty("Direction")] [JsonConverter(typeof(StringEnumConverter))]
        public OrderOperator Operator;
    }
}