using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TCAdminApiSharp.Querying.Operators;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying.Operations;

public class OrderList : List<OrderInfo>, IQueryOperation
{
    [JsonIgnore] public string JsonKey { get; set; } = "Order";

    public OrderList()
    {
    }

    public OrderList(string field, OrderOperator @operator)
    {
        Add(field, @operator);
    }

    public void Add(string column, OrderOperator @operator)
    {
        Add(new OrderInfo
        {
            Column = column,
            Operator = @operator
        });
    }

    public JToken GenerateQuery()
    {
        return JToken.FromObject(this);
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
}