using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TCAdminApiSharp.Querying.Structs;

namespace TCAdminApiSharp.Querying
{
    public class WhereList : List<WhereInfo>
    {
        public WhereOperator WhereOperator { get; set; }

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

        public override string ToString()
        {
            return this.Aggregate("",
                (current, whereInfo) =>
                    current +
                    $"([{whereInfo.Column}] {whereInfo.ColumnOperator.ToString()} '{whereInfo.ColumnValue}')");
        }
    }
}