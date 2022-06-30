using System.Net.Http;

namespace TCAdminApiSharp.Querying;

public interface IQueryOperation
{
    string JsonKey { get; set; }
    void ModifyRequest(HttpRequestMessage request);
}