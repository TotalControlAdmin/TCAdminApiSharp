using Newtonsoft.Json;
using TCAdminApiSharp.Converters;

namespace TCAdminApiSharp.Querying
{
    public class QueryableInfo
    {
        [JsonProperty("RowCount")] public int RowCount { get; set; }

        [JsonProperty("Offset")] public int Offset { get; set; }

        [JsonProperty("Where")]
        [JsonConverter(typeof(ToStringJsonConverter))]
        public WhereList WhereList { get; set; }

        public QueryableInfo()
        {
        }

        public QueryableInfo(int rowCount, int offset, WhereList whereList)
        {
            RowCount = rowCount;
            Offset = offset;
            WhereList = whereList;
        }

        public QueryableInfo(WhereList whereList)
        {
            WhereList = whereList;
        }

        public string BuildQuery()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}