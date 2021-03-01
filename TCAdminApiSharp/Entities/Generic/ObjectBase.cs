using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Generic
{
    public class ObjectBase
    {
        [JsonProperty("AppData")] public TcaXmlField AppData { get; set; }

        [JsonProperty("CreatedOn")] public DateTime CreatedOn { get; set; }

        [JsonProperty("CreatedBy")] public int CreatedBy { get; set; }

        [JsonProperty("ModifiedOn")] public DateTime ModifiedOn { get; set; }

        [JsonProperty("ModifiedBy")] public int ModifiedBy { get; set; }
    }

    public interface IObjectBaseCrud<out T>
    {
        public Task<bool> Update(Action<T> action);

        public Task<bool> Delete();
    }
}