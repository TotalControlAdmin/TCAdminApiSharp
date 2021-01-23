using Newtonsoft.Json.Linq;

namespace TCAdminApiSharp.Querying
{
    public interface IQueryOperation
    {
        string JsonKey { get; set; }

        JToken GenerateQuery();
    }
}