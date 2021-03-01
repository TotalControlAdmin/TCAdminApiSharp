using System.Net;
using System.Threading.Tasks;
using RestSharp;
using TCAdminApiSharp.Entities.API;
using TCAdminApiSharp.Entities.Task;
using TCAdminApiSharp.Entities.User;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;
using Task = TCAdminApiSharp.Entities.Task.Task;

namespace TCAdminApiSharp.Controllers
{
    public class TasksController : BaseController
    {
        public TasksController() : base("api/task")
        {
        }

        public async Task<Task> GetTask(int taskId)
        {
            try
            {
                var request = GenerateDefaultRequest();
                request.Resource += taskId;
                return (await ExecuteBaseResponseRequest<Task>(request)).Result;
            }
            catch (ApiResponseException e)
            {
                if (e.ErrorResponse.RestResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException(typeof(Task), e, new[] {taskId});

                throw;
            }
        }

        public Task<ListResponse<Task>> GetPendingTasks(int serverId)
        {
            var request = GenerateDefaultRequest();
            request.Resource += "getpendingtasks";
            request.AddParameter("serverId", serverId, ParameterType.QueryString);
            return ExecuteListResponseRequest<Task>(request);
        }

        public Task<ListResponse<TaskStep>> GetTaskSteps(int taskId)
        {
            var request = GenerateDefaultRequest();
            request.Resource = "api/taskstep/getsteps";
            request.AddParameter("taskId", taskId, ParameterType.QueryString);
            return ExecuteListResponseRequest<TaskStep>(request);
        }
    }
}