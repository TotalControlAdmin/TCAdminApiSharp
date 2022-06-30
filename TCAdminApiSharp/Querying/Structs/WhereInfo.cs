using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TCAdminApiSharp.Querying.Operators;

namespace TCAdminApiSharp.Querying.Structs;

public struct WhereInfo
{
    public string Column;
    public object ColumnValue;

    [JsonConverter(typeof(StringEnumConverter))]
    public ColumnOperator ColumnOperator;
}