﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Task
{
    public class TaskStep
    {
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