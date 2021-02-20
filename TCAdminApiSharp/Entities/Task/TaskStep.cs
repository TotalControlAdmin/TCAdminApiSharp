using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Entities.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace TCAdminApiSharp.Entities.Task
{
    public class TaskStep : ObjectBase
    {
        [JsonIgnore] public static readonly TasksController Controller =
            TcaClient.ServiceProvider.GetService<TasksController>() ?? throw new InvalidOperationException();
        
        [JsonProperty("ModuleId")]
        public string ModuleId { get; set; }

        [JsonProperty("ProcessId")]
        public int ProcessId { get; set; }

        [JsonProperty("TaskId")]
        public int TaskId { get; set; }

        [JsonProperty("StepId")]
        public int StepId { get; set; }

        [JsonProperty("ServerId")]
        public int ServerId { get; set; }

        [JsonProperty("Log")]
        public Dictionary<DateTime, string> Log { get; set; }

        [JsonProperty("DebugLog")]
        public Dictionary<DateTime, string> DebugLog { get; set; }

        [JsonProperty("LastDebugLogKey")]
        public DateTime LastDebugLogKey { get; set; }

        [JsonProperty("LastLogKey")]
        public DateTime LastLogKey { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LastLogItem")]
        public string LastLogItem { get; set; }

        [JsonProperty("Arguments")]
        public string Arguments { get; set; }

        [JsonProperty("ReturnValue")]
        public string ReturnValue { get; set; }

        [JsonProperty("Progress")]
        public int Progress { get; set; }
    }
}