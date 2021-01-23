using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            this.Add(field, @operator);
        }

        public void Add(string column, OrderOperator @operator) => this.Add(new OrderInfo
        {
            Column = column,
            Operator = @operator
        });

        public JToken GenerateQuery()
        {
            return JToken.FromObject(this);
        }
    }
}