using System;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Service.FileManager;

public class FileInfo
{
    [JsonProperty("Attributes")]
    public long Attributes { get; set; }

    [JsonProperty("CreationTime")]
    public DateTimeOffset CreationTime { get; set; }

    [JsonProperty("CreationTimeUtc")]
    public DateTimeOffset CreationTimeUtc { get; set; }

    [JsonProperty("Exists")]
    public bool Exists { get; set; }

    [JsonProperty("Extension")]
    public string Extension { get; set; }

    [JsonProperty("FullName")]
    public string FullName { get; set; }

    [JsonProperty("LastAccessTime")]
    public DateTimeOffset LastAccessTime { get; set; }

    [JsonProperty("LastAccessTimeUtc")]
    public DateTimeOffset LastAccessTimeUtc { get; set; }

    [JsonProperty("LastWriteTime")]
    public DateTimeOffset LastWriteTime { get; set; }

    [JsonProperty("LastWriteTimeUtc")]
    public DateTimeOffset LastWriteTimeUtc { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Directory")]
    public string Directory { get; set; }

    [JsonProperty("IsReadOnly")]
    public bool IsReadOnly { get; set; }

    [JsonProperty("Length")]
    public long Length { get; set; }

    [JsonProperty("LinuxExecutable")]
    public bool LinuxExecutable { get; set; }

    [JsonIgnore]
    public FileManagerService FileManagerService { get; internal set; }
    
    public System.Threading.Tasks.Task<bool> Rename(string newName) => FileManagerService.Rename(this.FullName, newName);
    public System.Threading.Tasks.Task<bool> Move(string target, bool overwrite = true) => FileManagerService.Move(new []{this.FullName}, target, overwrite);
    public System.Threading.Tasks.Task<bool> Copy(string target, bool overwrite = true) => FileManagerService.Copy(new []{this.FullName}, target, overwrite);
    public System.Threading.Tasks.Task<CompressModel> Compress(string target) => FileManagerService.Compress(new []{this.FullName}, target);
    public System.Threading.Tasks.Task<bool> Extract(string target) => FileManagerService.Extract(this.FullName, target);
}