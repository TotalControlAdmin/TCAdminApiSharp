using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCAdminApiSharp.Converters;

namespace TCAdminApiSharp.Querying
{
    public class QueryableInfo
    {
        [JsonProperty("RowCount")] public int RowCount { get; set; }

        [JsonProperty("Offset")] public int Offset { get; set; }

        [JsonIgnore] public List<IQueryOperation> QueryOperations { get; set; } = new();

        public QueryableInfo()
        {
        }

        public QueryableInfo(int rowCount, int offset, WhereList whereList)
        {
            RowCount = rowCount;
            Offset = offset;
            QueryOperations.Add(whereList);
        }

        public QueryableInfo(WhereList whereList)
        {
            QueryOperations.Add(whereList);
        }

        public string BuildQuery()
        {
            var jObject = JObject.FromObject(this);
            foreach (var queryOperation in QueryOperations)
            {
                jObject.Add(queryOperation.JsonKey, queryOperation.GenerateQuery());
            }
            return JsonConvert.SerializeObject(jObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}