using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Entities.Generic;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace TCAdminApiSharp.Entities.Task
{
    public class Task : ObjectBase
    {
        [JsonIgnore] public static readonly TasksController Controller =
            TcaClient.ServiceProvider.GetService<TasksController>() ?? throw new InvalidOperationException();

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

        [JsonIgnore] public IEnumerable<TaskStep> Steps => (List<TaskStep>) Controller.GetTaskSteps(this.TaskId).GetAwaiter().GetResult().Result;

        [JsonIgnore]
        public TaskStep CurrentStep
        {
            get { return Steps.FirstOrDefault(x => x.StepId == this.CurrentStepId) ?? throw new InvalidOperationException(); }
        }

        public async System.Threading.Tasks.Task Start()
        {
            var request = Controller.GenerateDefaultRequest();
            request.Resource += $"start/{this.TaskId}";
            request.Method = Method.POST;
            await Controller.ExecuteBaseResponseRequest(request);
        }
        
        public async System.Threading.Tasks.Task Cancel()
        {
            var request = Controller.GenerateDefaultRequest();
            request.Resource += $"cancel/{this.TaskId}";
            request.Method = Method.POST;
            await Controller.ExecuteBaseResponseRequest(request);
        }
    }
}