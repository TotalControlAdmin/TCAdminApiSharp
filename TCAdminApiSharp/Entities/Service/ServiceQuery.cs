using System.Collections.Generic;
using Newtonsoft.Json;
using TCAdminApiSharp.Entities.Generic;

namespace TCAdminApiSharp.Entities.Service;

public class ServiceQuery
{
    [JsonProperty("Running")]
    public bool Running { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Map")]
    public string Map { get; set; }

    [JsonProperty("Game")]
    public string Game { get; set; }

    [JsonProperty("GameType")]
    public string GameType { get; set; }

    [JsonProperty("MaxPlayers")]
    public long MaxPlayers { get; set; }

    [JsonProperty("NumPlayers")]
    public long NumPlayers { get; set; }

    [JsonProperty("MaxSpectators")]
    public long MaxSpectators { get; set; }

    [JsonProperty("NumSpectators")]
    public long NumSpectators { get; set; }

    [JsonProperty("Players", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> Players { get; set; }

    [JsonProperty("Rules", NullValueHandling = NullValueHandling.Ignore)]
    public TcaXmlField Rules { get; set; }
}