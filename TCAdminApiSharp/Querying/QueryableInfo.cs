using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Querying;

public class QueryableInfo
{
    [JsonProperty("RowCount")] public int RowCount { get; set; }

    [JsonProperty("Offset")] public int Offset { get; set; }

    [JsonIgnore] public List<IQueryOperation> QueryOperations { get; set; } = new();

    public QueryableInfo()
    {
    }

    public QueryableInfo(int rowCount, int offset, IQueryOperation queryOperation)
    {
        RowCount = rowCount;
        Offset = offset;
        QueryOperations.Add(queryOperation);
    }

    public QueryableInfo(params IQueryOperation[] queryOperations)
    {
        foreach (var queryOperation in queryOperations) QueryOperations.Add(queryOperation);
    }

    public void BuildQuery(HttpRequestMessage request)
    {
        foreach (var queryOperation in QueryOperations)
        {
            queryOperation.ModifyRequest(request);
        }
    }
}