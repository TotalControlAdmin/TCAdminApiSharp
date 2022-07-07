using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCAdminApiSharp.Entities.Billing;
using TCAdminApiSharp.Entities.Generic;
using TCAdminApiSharp.Entities.Service.FileManager;
using TCAdminApiSharp.Helpers;
// ReSharper disable UnusedMember.Global

namespace TCAdminApiSharp.Entities.Service;

public class Service : ObjectBase, IObjectBaseCrud<Service>, IPowerable
{
    [JsonProperty("EnableGameSwitching")] public bool EnableGameSwitching { get; set; }

    [JsonProperty("DisableQueryMonitoring")]
    public bool DisableQueryMonitoring { get; set; }

    [JsonProperty("DisableSlotMonitoring")]
    public bool DisableSlotMonitoring { get; set; }

    [JsonProperty("DisablePrivateMonitoring")]
    public bool DisablePrivateMonitoring { get; set; }

    [JsonProperty("DisableBrandedMonitoring")]
    public bool DisableBrandedMonitoring { get; set; }

    [JsonProperty("GameSwitchingAllowedGames")]
    public IList<string> GameSwitchingAllowedGames { get; set; }

    [JsonProperty("FastDownloadUsage")] public int FastDownloadUsage { get; set; }

    [JsonProperty("FastDownloadFileCount")]
    public int FastDownloadFileCount { get; set; }

    [JsonProperty("ServiceId")] public int ServiceId { get; set; }

    [JsonProperty("MemoryLimitMB")] public int MemoryLimitMB { get; set; }

    [JsonProperty("CpuLimit")] public int CpuLimit { get; set; }

    [JsonProperty("ServerId")] public int ServerId { get; set; }

    [JsonProperty("BillingId")] public string BillingId { get; set; }

    [JsonProperty("BillingStatus")] public BillingStatus BillingStatus { get; set; }

    [JsonProperty("GameId")] public int GameId { get; set; }

    [JsonProperty("UserPackageId")] public int UserPackageId { get; set; }

    [JsonProperty("UserId")] public int UserId { get; set; }

    [JsonProperty("Executable")] public string? Executable { get; set; }

    [JsonProperty("UnparsedCommandLine")] public string UnparsedCommandLine { get; set; }

    [JsonProperty("WorkingDirectory")] public string WorkingDirectory { get; set; }

    [JsonProperty("RootDirectory")] public string RootDirectory { get; set; }

    [JsonProperty("Private")] public bool Private { get; set; }

    [JsonProperty("Slots")] public int Slots { get; set; }

    [JsonProperty("CurrentOnline")] public bool CurrentOnline { get; set; }

    [JsonProperty("CurrentPid")] public int CurrentPid { get; set; }

    [JsonProperty("CurrentPlayers")] public int CurrentPlayers { get; set; }

    [JsonProperty("CurrentMaxPlayers")] public int CurrentMaxPlayers { get; set; }

    [JsonProperty("CurrentCpu")] public double CurrentCpu { get; set; }

    [JsonProperty("CurrentMemory")] public double CurrentMemory { get; set; }

    [JsonProperty("CurrentMemoryLimit")] public double CurrentMemoryLimit { get; set; }

    [JsonProperty("CurrentMemoryPercent")] public double CurrentMemoryPercent { get; set; }

    [JsonProperty("CurrentBandwidth")] public double CurrentBandwidth { get; set; }

    [JsonProperty("CurrentMap")] public string CurrentMap { get; set; }

    [JsonProperty("CurrentGameType")] public string CurrentGameType { get; set; }

    [JsonProperty("CurrentName")] public string CurrentName { get; set; }

    [JsonProperty("ServiceStatus")] public EServiceStatus EServiceStatus { get; set; }

    [JsonProperty("StartTime")] public DateTime StartTime { get; set; }

    [JsonProperty("JoinUrl")] public string JoinUrl { get; set; }

    [JsonProperty("Branded")] public bool Branded { get; set; }

    [JsonProperty("ConnectionInfo")] public string ConnectionInfo { get; set; }

    [JsonProperty("QueryInfo")] public string QueryInfo { get; set; }

    [JsonProperty("RConInfo")] public string RConInfo { get; set; }

    [JsonProperty("FtpInfo")] public string FtpInfo { get; set; }

    [JsonProperty("IpAddress")] public string IpAddress { get; set; }

    [JsonProperty("IpHostname")] public string IpHostname { get; set; }

    [JsonProperty("PublicIpAddress")] public string PublicIpAddress { get; set; }

    [JsonProperty("GamePort")] public int GamePort { get; set; }

    [JsonProperty("QueryPort")] public int QueryPort { get; set; }

    [JsonProperty("RConPort")] public int RConPort { get; set; }

    [JsonProperty("CustomPort1")] public int CustomPort1 { get; set; }

    [JsonProperty("CustomPort2")] public int CustomPort2 { get; set; }

    [JsonProperty("CustomPort3")] public int CustomPort3 { get; set; }

    [JsonProperty("CustomPort4")] public int CustomPort4 { get; set; }

    [JsonProperty("CustomPort5")] public int CustomPort5 { get; set; }

    [JsonProperty("CustomPort6")] public int CustomPort6 { get; set; }

    [JsonProperty("CustomPort7")] public int CustomPort7 { get; set; }

    [JsonProperty("CustomPort8")] public int CustomPort8 { get; set; }

    [JsonProperty("CustomPort9")] public int CustomPort9 { get; set; }

    [JsonProperty("CustomPort10")] public int CustomPort10 { get; set; }

    [JsonProperty("CustomPort11")] public int CustomPort11 { get; set; }

    [JsonProperty("CustomPort12")] public int CustomPort12 { get; set; }

    [JsonProperty("CustomPort13")] public int CustomPort13 { get; set; }

    [JsonProperty("CustomPort14")] public int CustomPort14 { get; set; }

    [JsonProperty("CustomPort15")] public int CustomPort15 { get; set; }

    [JsonProperty("CustomPort16")] public int CustomPort16 { get; set; }

    [JsonProperty("CustomPort17")] public int CustomPort17 { get; set; }

    [JsonProperty("CustomPort18")] public int CustomPort18 { get; set; }

    [JsonProperty("CustomPort19")] public int CustomPort19 { get; set; }

    [JsonProperty("CustomPort20")] public int CustomPort20 { get; set; }

    [JsonProperty("CustomPort21")] public int CustomPort21 { get; set; }

    [JsonProperty("CustomPort22")] public int CustomPort22 { get; set; }

    [JsonProperty("CustomPort23")] public int CustomPort23 { get; set; }

    [JsonProperty("CustomPort24")] public int CustomPort24 { get; set; }

    [JsonProperty("CustomPort25")] public int CustomPort25 { get; set; }

    [JsonProperty("CustomPort26")] public int CustomPort26 { get; set; }

    [JsonProperty("CustomPort27")] public int CustomPort27 { get; set; }

    [JsonProperty("CustomPort28")] public int CustomPort28 { get; set; }

    [JsonProperty("CustomPort29")] public int CustomPort29 { get; set; }

    [JsonProperty("CustomPort30")] public int CustomPort30 { get; set; }

    [JsonProperty("CustomPort31")] public int CustomPort31 { get; set; }

    [JsonProperty("CustomPort32")] public int CustomPort32 { get; set; }

    [JsonProperty("CustomPort33")] public int CustomPort33 { get; set; }

    [JsonProperty("CustomPort34")] public int CustomPort34 { get; set; }

    [JsonProperty("CustomPort35")] public int CustomPort35 { get; set; }

    [JsonProperty("CustomPort36")] public int CustomPort36 { get; set; }

    [JsonProperty("CustomPort37")] public int CustomPort37 { get; set; }

    [JsonProperty("CustomPort38")] public int CustomPort38 { get; set; }

    [JsonProperty("CustomPort39")] public int CustomPort39 { get; set; }

    [JsonProperty("CustomPort40")] public int CustomPort40 { get; set; }

    [JsonProperty("CustomPort41")] public int CustomPort41 { get; set; }

    [JsonProperty("CustomPort42")] public int CustomPort42 { get; set; }

    [JsonProperty("CustomPort43")] public int CustomPort43 { get; set; }

    [JsonProperty("CustomPort44")] public int CustomPort44 { get; set; }

    [JsonProperty("CustomPort45")] public int CustomPort45 { get; set; }

    [JsonProperty("CustomPort46")] public int CustomPort46 { get; set; }

    [JsonProperty("CustomPort47")] public int CustomPort47 { get; set; }

    [JsonProperty("CustomPort48")] public int CustomPort48 { get; set; }

    [JsonProperty("CustomPort49")] public int CustomPort49 { get; set; }

    [JsonProperty("CustomPort50")] public int CustomPort50 { get; set; }

    [JsonProperty("Startup")] public ServiceStartup Startup { get; set; }

    [JsonProperty("Priority")] public int Priority { get; set; }

    [JsonProperty("Affinity")] public int Affinity { get; set; }

    [JsonProperty("NumaNode")] public int NumaNode { get; set; }

    [JsonProperty("GameKey")] public string GameKey { get; set; }

    [JsonProperty("Variables")] public TcaXmlField Variables { get; set; } = new();

    [JsonProperty("InstalledMods")] public IList<string> InstalledMods { get; set; }

    [JsonProperty("InstalledUpdate")] public string InstalledUpdate { get; set; }

    [JsonProperty("OverrideCommandLine")] public bool OverrideCommandLine { get; set; }

    [JsonProperty("Notes")] public string Notes { get; set; }

    [JsonIgnore] public FileManagerService FileManagerService => new(this);
    
    public async Task<bool> Update(Action<Service> action)
    {
        var service = new Service();
        action(service);
        var putJson = JsonConvert.SerializeObject(service, Constants.IgnoreDefaultValues);
        var request = TcaClient.ServicesController.GenerateDefaultRequest(ServiceId.ToString());
        request.Method = HttpMethod.Put;
        request.Content = new StringContent(putJson, Encoding.UTF8, Constants.JsonContentType);
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public Task<bool> Delete()
    {
        // todo: Luis hurry up implement pls
        throw new NotImplementedException();
    }

    public async Task<bool> Start(string reason = "")
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Start));
        request.Method = HttpMethod.Post;
        if (!string.IsNullOrEmpty(reason))
        {
            request.Content = new FormUrlEncodedContent(new[]
                { new KeyValuePair<string, string>(nameof(reason), reason) });
        }
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public async Task<bool> Restart(string reason = "")
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Restart));
        request.Method = HttpMethod.Post;
        if (!string.IsNullOrEmpty(reason))
        {
            request.Content = new FormUrlEncodedContent(new[]
                { new KeyValuePair<string, string>(nameof(reason), reason) });
        }
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public async Task<bool> Stop(string reason = "")
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Stop));
        request.Method = HttpMethod.Post;
        if (!string.IsNullOrEmpty(reason))
        {
            request.Content = new FormUrlEncodedContent(new[]
                { new KeyValuePair<string, string>(nameof(reason), reason) });
        }
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public async Task<bool> Configure()
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Configure));
        request.Method = HttpMethod.Post;
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public async Task<bool> Suspend()
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Suspend));
        request.Method = HttpMethod.Post;
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }

    public async Task<bool> Unsuspend()
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), nameof(Unsuspend));
        request.Method = HttpMethod.Post;
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest(request)).Success;
    }
    
    public async Task<ServiceStatus> GetStatus()
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), "Status");
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest<ServiceStatus>(request)).Result;
    }
    
    public async Task<ServiceQuery> GetQuery()
    {
        var request = TcaClient.ServicesController.GenerateDefaultRequest( ServiceId.ToString(), "Query");
        return (await TcaClient.ServicesController.ExecuteBaseResponseRequest<ServiceQuery>(request)).Result;
    }
}