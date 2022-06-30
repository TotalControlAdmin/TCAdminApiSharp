using System;
using Newtonsoft.Json;

namespace TCAdminApiSharp.Entities.Service.FileManager;

public class DirectoryListing
{
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Size")]
    public long Size { get; set; }

    [JsonProperty("Path")]
    public string Path { get; set; }

    [JsonProperty("Extension")]
    public string Extension { get; set; }

    [JsonProperty("IsDirectory")]
    public bool IsDirectory { get; set; }

    [JsonIgnore] public bool IsFile => !IsDirectory;

    [JsonProperty("HasDirectories")]
    public bool HasDirectories { get; set; }

    [JsonProperty("Created")]
    public DateTimeOffset Created { get; set; }

    [JsonProperty("CreatedUtc")]
    public DateTimeOffset CreatedUtc { get; set; }

    [JsonProperty("Modified")]
    public DateTimeOffset Modified { get; set; }

    [JsonProperty("ModifiedUtc")]
    public DateTimeOffset ModifiedUtc { get; set; }

    [JsonIgnore]
    public FileManagerService FileManagerService { get; internal set; }

    public System.Threading.Tasks.Task<bool> Rename(string newName) => FileManagerService.Rename(this.Path, newName);
    public System.Threading.Tasks.Task<bool> Move(string target, bool overwrite = true) => FileManagerService.Move(new []{this.Path}, target, overwrite);
    public System.Threading.Tasks.Task<bool> Copy(string target, bool overwrite = true) => FileManagerService.Copy(new []{this.Path}, target, overwrite);
    public System.Threading.Tasks.Task<CompressModel> Compress(string target) => FileManagerService.Compress(new []{this.Path}, target);
}