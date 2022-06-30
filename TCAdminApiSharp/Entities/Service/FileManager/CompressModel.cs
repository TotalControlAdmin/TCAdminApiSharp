using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Service.FileManager;

public class CompressModel
{
    [JsonProperty("Zip")]
    public string Zip { get; set; }
}