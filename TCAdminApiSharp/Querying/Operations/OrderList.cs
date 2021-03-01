using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TCAdminApiSharp.Querying.Operators;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying.Operations
{
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

        public void ModifyRequest(IRestRequest request)
        {
            JObject jObject = new();
            var queryInfoExists = request.Parameters.Any(x => x.Name == "queryInfo");
            if (queryInfoExists)
            {
                jObject = JsonConvert.DeserializeObject<JObject>(request.Parameters.Find(x => x.Name == "queryInfo")!.Value!.ToString()!);
            }
            
            jObject[JsonKey] = JToken.FromObject(this);

            request.AddOrUpdateParameter("queryInfo", jObject, ParameterType.GetOrPost);
        }
    }
}