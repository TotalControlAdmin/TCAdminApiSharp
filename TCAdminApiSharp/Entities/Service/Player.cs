using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Service;

public class Player
{
    [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("Ping", NullValueHandling = NullValueHandling.Ignore)]
    public int Ping { get; set; }
}