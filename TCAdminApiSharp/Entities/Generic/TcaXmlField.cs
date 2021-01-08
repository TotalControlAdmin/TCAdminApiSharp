using System.Collections.Generic;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Generic
{
    public class TcaXmlField
    {
        [JsonProperty("Values")] public Dictionary<string, object> Values { get; set; }

        [JsonProperty("Keys")] public IList<string> Keys { get; set; }
    }
}