using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Task;
using Task = TCAdminApiSharp.Entities.Task.Task;

namespace TCAdminApiSharp.Controllers;

public class TasksController : BaseController
{
    public TasksController(TcaClient tcaClient) : base(tcaClient, "api/task")
    {
    }

    public async Task<Task> GetTask(int taskId)
    {
        var request = GenerateDefaultRequest(taskId.ToString());
        return (await ExecuteBaseResponseRequest<Task>(request)).Result;
    }

    public Task<ListResponse<Task>> GetPendingTasks(int serverId)
    {
        var request = GenerateDefaultRequest(QueryHelpers.AddQueryString("getpendingtasks", nameof(serverId), serverId.ToString()));
        return ExecuteListResponseRequest<Task>(request);
    }

    public Task<ListResponse<TaskStep>> GetTaskSteps(int taskId)
    {
        var request = GenerateDefaultRequest();
        request.RequestUri = new Uri(QueryHelpers.AddQueryString("api/taskstep/getsteps", nameof(taskId), taskId.ToString()), UriKind.RelativeOrAbsolute);
        return ExecuteListResponseRequest<TaskStep>(request);
    }
}