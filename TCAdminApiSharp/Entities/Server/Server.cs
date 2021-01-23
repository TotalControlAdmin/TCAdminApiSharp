using System;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.DependencyInjection;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Entities.Generic;
using TCAdminApiSharp.Helpers;

namespace TCAdminApiSharp.Entities.Server
{
    public class Server : ObjectBase, IObjectBaseCrud<Server>
    {
        [JsonIgnore] public readonly ServersController ServersController =
            TcaClient.ServiceProvider.GetService<ServersController>() ?? throw new InvalidOperationException();

        [JsonProperty("DisableNewServices")] public bool DisableNewServices { get; set; }

        [JsonProperty("GameFilesPath")] public string GameFilesPath { get; set; }

        [JsonProperty("VSUseCustomGameFiles")] public bool VSUseCustomGameFiles { get; set; }

        [JsonProperty("ExtractSpeed")] public int ExtractSpeed { get; set; }

        [JsonProperty("UserFilesPath")] public string UserFilesPath { get; set; }

        [JsonProperty("FolderTemplate")] public string FolderTemplate { get; set; }

        [JsonProperty("KeepLocalCopy")] public bool KeepLocalCopy { get; set; }

        [JsonProperty("LastServiceCreationUtc")]
        public DateTime LastServiceCreationUtc { get; set; }

        [JsonProperty("EnableFileSharing")] public bool EnableFileSharing { get; set; }

        [JsonProperty("EnableFastDownload")] public bool EnableFastDownload { get; set; }

        [JsonProperty("ServerFileProviderModuleId")]
        public string ServerFileProviderModuleId { get; set; }

        [JsonProperty("ServerFileProviderId")] public int ServerFileProviderId { get; set; }

        [JsonProperty("ClientFileProviderModuleId")]
        public string ClientFileProviderModuleId { get; set; }

        [JsonProperty("ClientFileProviderId")] public int ClientFileProviderId { get; set; }

        [JsonProperty("ClientFileProviderSourceId")]
        public int ClientFileProviderSourceId { get; set; }

        [JsonProperty("FastDownloadFolderTemplate")]
        public string FastDownloadFolderTemplate { get; set; }

        [JsonProperty("FastDownloadSavePath")] public string FastDownloadSavePath { get; set; }

        [JsonProperty("FastDownloadUrl")] public string FastDownloadUrl { get; set; }

        [JsonProperty("ClientFastDownloadProviderModuleId")]
        public string ClientFastDownloadProviderModuleId { get; set; }

        [JsonProperty("ClientFastDownloadProviderId")]
        public int ClientFastDownloadProviderId { get; set; }

        [JsonProperty("ClientFastDownloadProviderSourceId")]
        public int ClientFastDownloadProviderSourceId { get; set; }

        [JsonProperty("NumberOfServices")] public int NumberOfServices { get; set; }

        [JsonProperty("NumberOfVoiceServices")]
        public int NumberOfVoiceServices { get; set; }

        [JsonProperty("NumberOfSlots")] public int NumberOfSlots { get; set; }

        [JsonProperty("NumberOfVoiceSlots")] public int NumberOfVoiceSlots { get; set; }

        [JsonProperty("MaxSlots")] public int MaxSlots { get; set; }

        [JsonProperty("SkipSuspendedServices")]
        public bool SkipSuspendedServices { get; set; }

        [JsonProperty("SeparateVoiceSots")] public bool SeparateVoiceSots { get; set; }

        [JsonProperty("MaxVoiceSlots")] public int MaxVoiceSlots { get; set; }

        [JsonProperty("MaxServices")] public int MaxServices { get; set; }

        [JsonProperty("SeparateVoiceServices")]
        public bool SeparateVoiceServices { get; set; }

        [JsonProperty("MaxVoiceServices")] public int MaxVoiceServices { get; set; }

        [JsonProperty("ServerId")] public int ServerId { get; set; }

        [JsonProperty("PrivateNetworkId")] public int PrivateNetworkId { get; set; }

        [JsonProperty("PrivateNetworkIp")] public string PrivateNetworkIp { get; set; }

        [JsonProperty("DisableVirtualServerUser")]
        public bool DisableVirtualServerUser { get; set; }

        [JsonProperty("UseCustomDatacenterId")]
        public bool UseCustomDatacenterId { get; set; }

        [JsonProperty("DatacenterId")] public int DatacenterId { get; set; }

        [JsonProperty("OwnerId")] public int OwnerId { get; set; }

        [JsonProperty("OwnerIdFriendlyName")] public string OwnerIdFriendlyName { get; set; }

        [JsonProperty("IsMaster")] public bool IsMaster { get; set; }

        [JsonProperty("OperatingSystem")] public int OperatingSystem { get; set; }

        [JsonProperty("PrimaryIp")] public string PrimaryIp { get; set; }

        [JsonProperty("SecurePort")] public int SecurePort { get; set; }

        [JsonProperty("StandardPort")] public int StandardPort { get; set; }

        [JsonProperty("BindAllIps")] public bool BindAllIps { get; set; }

        [JsonProperty("FirewallIp")] public string FirewallIp { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("Enabled")] public bool Enabled { get; set; }

        [JsonProperty("DisableUpdates")] public bool DisableUpdates { get; set; }

        [JsonProperty("MonitorVirtualDirectory")]
        public string MonitorVirtualDirectory { get; set; }

        [JsonProperty("PublicVirtualDirectory")]
        public string PublicVirtualDirectory { get; set; }

        [JsonProperty("ControlPanelEnableBasicAuth")]
        public bool ControlPanelEnableBasicAuth { get; set; }

        [JsonProperty("ControlPanelRealm")] public string ControlPanelRealm { get; set; }

        [JsonProperty("ControlPanelLogOutUrl")]
        public string ControlPanelLogOutUrl { get; set; }

        [JsonProperty("WindowsFirewallEnabled")]
        public bool WindowsFirewallEnabled { get; set; }

        [JsonProperty("uPnPPortForwardingEnabled")]
        public bool UPnPPortForwardingEnabled { get; set; }

        [JsonProperty("MonitorLogin")] public string MonitorLogin { get; set; }

        [JsonProperty("MonitorPassword")] public string MonitorPassword { get; set; }

        [JsonProperty("MonitorVersion")] public string MonitorVersion { get; set; }

        [JsonProperty("FileSystemComparison")] public int FileSystemComparison { get; set; }

        [JsonProperty("FileSystemDirectorySeparator")]
        public string FileSystemDirectorySeparator { get; set; }

        [JsonProperty("DatacenterName")] public string DatacenterName { get; set; }

        [JsonProperty("IsVirtual")] public bool IsVirtual { get; set; }

        [JsonProperty("RealServerId")] public int RealServerId { get; set; }

        [JsonProperty("RealServerName")] public string RealServerName { get; set; }

        [JsonProperty("KeepAlive")] public int KeepAlive { get; set; }

        [JsonProperty("NumberOfProcessors")] public int NumberOfProcessors { get; set; }

        [JsonProperty("MaxNumaNode")] public int MaxNumaNode { get; set; }

        [JsonProperty("MonitorAffinity")] public int MonitorAffinity { get; set; }

        [JsonProperty("MonitorNumaNode")] public int MonitorNumaNode { get; set; }

        [JsonProperty("MonitorPriority")] public int MonitorPriority { get; set; }

        [JsonProperty("Affinity")] public int Affinity { get; set; }

        [JsonProperty("NumaNode")] public int NumaNode { get; set; }

        [JsonProperty("Memory")] public long Memory { get; set; }

        [JsonProperty("MemoryMB")] public int MemoryMB { get; set; }

        [JsonProperty("MemoryString")] public string MemoryString { get; set; }

        [JsonProperty("DiskSpace")] public int DiskSpace { get; set; }

        [JsonProperty("DiskSpaceMB")] public int DiskSpaceMB { get; set; }

        [JsonProperty("DiskSpaceString")] public string DiskSpaceString { get; set; }

        [JsonProperty("DiskDrive")] public string DiskDrive { get; set; }

        [JsonProperty("CustomField1")] public string CustomField1 { get; set; }

        [JsonProperty("CustomField2")] public string CustomField2 { get; set; }

        [JsonProperty("CustomField3")] public string CustomField3 { get; set; }

        [JsonProperty("CustomField4")] public string CustomField4 { get; set; }

        [JsonProperty("CustomField5")] public string CustomField5 { get; set; }

        [JsonProperty("CustomField6")] public string CustomField6 { get; set; }

        [JsonProperty("CustomField7")] public string CustomField7 { get; set; }

        [JsonProperty("CustomField8")] public string CustomField8 { get; set; }

        [JsonProperty("CustomField9")] public string CustomField9 { get; set; }

        [JsonProperty("CustomField10")] public string CustomField10 { get; set; }

        [JsonProperty("CustomField11")] public string CustomField11 { get; set; }

        [JsonProperty("CustomField12")] public string CustomField12 { get; set; }

        [JsonProperty("CustomField13")] public string CustomField13 { get; set; }

        [JsonProperty("CustomField14")] public string CustomField14 { get; set; }

        [JsonProperty("CustomField15")] public string CustomField15 { get; set; }

        [JsonProperty("BillingId")] public string BillingId { get; set; }

        [JsonProperty("BillingStatus")] public int BillingStatus { get; set; }

        [JsonProperty("PeakMemoryMB")] public double PeakMemoryMB { get; set; }

        [JsonProperty("PeakCPU")] public double PeakCPU { get; set; }

        [JsonProperty("DCSyncUrl")] public string DCSyncUrl { get; set; }

        [JsonProperty("DCDownloadServer")] public string DCDownloadServer { get; set; }

        [JsonProperty("DCDownloadUser")] public string DCDownloadUser { get; set; }

        [JsonProperty("DCDownloadPassword")] public string DCDownloadPassword { get; set; }

        public void Update(Action<Server> action)
        {
            var server = new Server();
            action(server);
            var putJson = JsonConvert.SerializeObject(server, Constants.IgnoreDefaultValues);
            var request = ServersController.GenerateDefaultRequest();
            request.Resource += this.ServerId;
            request.Method = Method.PUT;
            request.AddParameter(Constants.JsonContentType, putJson, ParameterType.RequestBody);
            ServersController.ExecuteBaseResponseRequest(request);
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}