using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCAdminApiSharp.Entities.Generic;

namespace TCAdminApiSharp.Entities.Task;

public class Task : ObjectBase
{
    [JsonProperty("TaskId")] public int TaskId { get; set; }

    [JsonProperty("UserId")] public int UserId { get; set; }

    [JsonProperty("UserName")] public string UserName { get; set; }

    [JsonProperty("ServerId")] public int ServerId { get; set; }

    [JsonProperty("Name")] public string Name { get; set; }

    [JsonProperty("Status")] public TaskStatus Status { get; set; }

    [JsonProperty("CurrentStepId")] public int CurrentStepId { get; set; }

    [JsonProperty("LastRunTime")] public DateTime LastRunTime { get; set; }

    [JsonProperty("LastRunTimeUtc")] public DateTime LastRunTimeUtc { get; set; }

    [JsonProperty("ScheduledTime")] public DateTime ScheduledTime { get; set; }

    [JsonProperty("ScheduledTimeUtc")] public DateTime ScheduledTimeUtc { get; set; }

    [JsonProperty("FilterTime")] public DateTime FilterTime { get; set; }

    [JsonProperty("FilterTimeUtc")] public DateTime FilterTimeUtc { get; set; }

    [JsonProperty("Enabled")] public bool Enabled { get; set; }

    [JsonProperty("Source")] public string Source { get; set; }

    [JsonProperty("SourceId")] public string SourceId { get; set; }

    [JsonProperty("RecurringTaskGuid")] public object RecurringTaskGuid { get; set; }

    [JsonProperty("RecurringTaskId")] public int RecurringTaskId { get; set; }

    [JsonProperty("TotalSteps")] public int TotalSteps { get; set; }

    [JsonProperty("RedirectUrl")] public string RedirectUrl { get; set; }

    public async System.Threading.Tasks.Task Start()
    {
        var request = TcaClient.TasksController.GenerateDefaultRequest( TaskId.ToString(), nameof(Start));
        request.Method = HttpMethod.Post;
        await TcaClient.TasksController.ExecuteBaseResponseRequest(request);
    }

    public async System.Threading.Tasks.Task Cancel()
    {
        var request = TcaClient.TasksController.GenerateDefaultRequest(TaskId.ToString(), nameof(Cancel));
        request.Method = HttpMethod.Post;
        await TcaClient.TasksController.ExecuteBaseResponseRequest(request);
    }
        
    public async Task<IEnumerable<TaskStep>> GetSteps()
    {
        return (await TcaClient.TasksController.GetTaskSteps(TaskId)).Result;
    }
    
    public async Task<TaskStep> GetCurrentStep()
    {
        return (await GetSteps()).First(x => x.StepId == CurrentStepId);
    }
}