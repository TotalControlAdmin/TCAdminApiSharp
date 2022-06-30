using System.Threading.Tasks;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Server;

namespace TCAdminApiSharp.Controllers;

public class ServersController : BaseController
{
    public ServersController(TcaClient tcaClient) : base(tcaClient, "api/server")
    {
    }

    public async Task<Server> GetServer(int serverId)
    {
        var request = GenerateDefaultRequest(serverId.ToString());
        var result = (await ExecuteBaseResponseRequest<Server>(request)).Result;
        return result;
    }

    public async Task<ListResponse<Server>> GetServers()
    {
        var request = GenerateDefaultRequest();
        var result = await ExecuteListResponseRequest<Server>(request);

        return result;
    }
}