using System;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Service;

public class ServiceStatus
{
    [JsonProperty("StartTime")]
    public DateTimeOffset StartTime { get; set; }

    [JsonProperty("ServiceStatus")]
    public EServiceStatus Status { get; set; }

    [JsonProperty("ProcessId")]
    public long ProcessId { get; set; }

    [JsonProperty("ServiceId")]
    public long ServiceId { get; set; }

    [JsonProperty("BandwidthLastSecond")]
    public long BandwidthLastSecond { get; set; }

    [JsonProperty("CpuLastSecond")]
    public double CpuLastSecond { get; set; }

    [JsonProperty("MemoryLastSecond")]
    public long MemoryLastSecond { get; set; }

    [JsonProperty("MemoryPercentageLastSecond")]
    public double MemoryPercentageLastSecond { get; set; }
}