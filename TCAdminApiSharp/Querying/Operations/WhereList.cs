using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using TCAdminApiSharp.Querying.Operators;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying.Operations
{
    public class WhereList : List<WhereInfo>, IQueryOperation
    {
        [JsonIgnore] public string JsonKey { get; set; } = "Where";
        [JsonIgnore] public WhereOperator WhereOperator { get; set; }

        public WhereList() => this.WhereOperator = WhereOperator.And;

        public WhereList(WhereInfo where)
        {
            this.WhereOperator = WhereOperator.And;
            this.Add(where);
        }

        public WhereList(string column, object value)
        {
            this.WhereOperator = WhereOperator.And;
            this.Add(column, ColumnOperator.Equal, RuntimeHelpers.GetObjectValue(value));
        }

        public WhereList(string column, ColumnOperator @operator, object value)
        {
            this.WhereOperator = WhereOperator.And;
            this.Add(new WhereInfo
            {
                Column = column,
                ColumnOperator = @operator,
                ColumnValue = RuntimeHelpers.GetObjectValue(value)
            });
        }

        public void Add(string column, object value) =>
            this.Add(column, ColumnOperator.Equal, RuntimeHelpers.GetObjectValue(value));

        public void Add(string column, ColumnOperator @operator, object value) => this.Add(new WhereInfo
        {
            Column = column,
            ColumnOperator = @operator,
            ColumnValue = value
        });

        public JToken GenerateQuery()
        {
            var temp = "(";
            foreach (var info in this)
            {
                temp += $"[{info.Column}] {ConvertColumnOperator(info.ColumnOperator)} '{info.ColumnValue}'";
                if (!this.Last().Equals(info))
                {
                    // Add where operator
                    temp += $" {this.WhereOperator.ToString().ToUpper()} ";
                }
            }

            temp += ")";
            return new JValue(temp);
        }

        public string ConvertColumnOperator(ColumnOperator columnOperator)
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
}