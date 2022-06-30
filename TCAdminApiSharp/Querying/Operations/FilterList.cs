using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying.Operations;

public class FilterList : List<FilterInfo>, IQueryOperation
{
    [JsonIgnore]
    public string JsonKey { get; set; } = "Fields";

    public FilterList()
    {
    }
        
    public FilterList(params string[] where)
    {
        foreach (var filterInfo in where)
        {
            Add(filterInfo);
        }
    }

    public FilterList(FilterInfo where)
    {
        Add(where);
    }
        
    public FilterList(params FilterInfo[] where)
    {
        foreach (var filterInfo in @where)
        {
            Add(filterInfo);
        }
    }

    public void Add(string column)
    {
        Add(new FilterInfo
        {
            Column = column
        });
    }

    public JToken GenerateQuery()
    {
        var temp = this.Aggregate("(", (current, info) => current + $"[{info.Column}]") + ")";
        return new JValue(temp);
    }

    public void ModifyRequest(HttpRequestMessage request)
    {
        request.RequestUri = new Uri(QueryHelpers.AddQueryString(request.RequestUri.ToString(), JsonKey, GenerateQuery().ToString()));
    }
}