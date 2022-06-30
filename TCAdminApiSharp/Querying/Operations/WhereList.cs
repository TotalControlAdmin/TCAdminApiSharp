using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCAdminApiSharp.Querying.Operators;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying.Operations;

public class WhereList : List<WhereInfo>, IQueryOperation
{
    [System.Text.Json.Serialization.JsonIgnore] public string JsonKey { get; set; } = "Where";
    [System.Text.Json.Serialization.JsonIgnore] public WhereOperator WhereOperator { get; set; }

    public WhereList()
    {
        WhereOperator = WhereOperator.And;
    }

    public WhereList(WhereInfo where)
    {
        WhereOperator = WhereOperator.And;
        Add(where);
    }

    public WhereList(string column, object value)
    {
        WhereOperator = WhereOperator.And;
        Add(column, ColumnOperator.Equal, RuntimeHelpers.GetObjectValue(value));
    }

    public WhereList(string column, ColumnOperator @operator, object value)
    {
        WhereOperator = WhereOperator.And;
        Add(new WhereInfo
        {
            Column = column,
            ColumnOperator = @operator,
            ColumnValue = RuntimeHelpers.GetObjectValue(value)
        });
    }

    public void Add(string column, object value)
    {
        Add(column, ColumnOperator.Equal, RuntimeHelpers.GetObjectValue(value));
    }

    public void Add(string column, ColumnOperator @operator, object value)
    {
        Add(new WhereInfo
        {
            Column = column,
            ColumnOperator = @operator,
            ColumnValue = value
        });
    }

    public JToken GenerateQuery()
    {
        var temp = "(";
        foreach (var info in this)
        {
            var tempColumnName = info.Column;
            if (!tempColumnName.StartsWith("[") && !tempColumnName.EndsWith("]"))
            {
                tempColumnName = $"[{info.Column}]";
            }
                
            temp += $"{tempColumnName} {ConvertColumnOperator(info.ColumnOperator)} '{info.ColumnValue}'";
            if (!this.Last().Equals(info))
                // Add where operator
                temp += $" {WhereOperator.ToString().ToUpper()} ";
        }

        temp += ")";
        return new JValue(temp);
    }

    public void ModifyRequest(HttpRequestMessage request)
    {
        JObject jObject = new();
        var dictionary = QueryHelpers.ParseQuery(request.RequestUri.ToString());
        var queryInfoExists = dictionary.Any(x => x.Key == "queryInfo");
        if (queryInfoExists)
        {
            jObject = JsonConvert.DeserializeObject<JObject>(dictionary.FirstOrDefault(x => x.Key == "queryInfo").Value.ToString()!);
        }

        jObject[JsonKey] = GenerateQuery();
        request.RequestUri =
            new Uri(QueryHelpers.AddQueryString(request.RequestUri.ToString(), "queryInfo", jObject.ToString()));
    }

    public static string ConvertColumnOperator(ColumnOperator columnOperator)
    {
        return columnOperator switch
        {
            ColumnOperator.Equal => "=",
            ColumnOperator.NotEqual => "<>",
            ColumnOperator.GreaterThan => ">",
            ColumnOperator.GreaterOrEqualTo => ">=",
            ColumnOperator.LowerThan => "<",
            ColumnOperator.LowerOrEqualTo => "<=",
            ColumnOperator.Like => "LIKE",
            ColumnOperator.NotLike => "NOT LIKE",
            _ => throw new ArgumentOutOfRangeException(nameof(columnOperator), columnOperator, null)
        };
    }
}