using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using TCAdminApiSharp.Entities.Generic;

namespace TCAdminApiSharp.Entities.Service.FileManager;

public class FileManagerService : ITCAdminClientCompatible
{
    public TcaClient TcaClient { get; set; }
    private readonly Service _service;
    private string FileManagerEndpoint => _service.ServiceId + "/FileManager";

    public FileManagerService(TcaClient tcaClient, Service service)
    {
        _service = service;
        TcaClient = tcaClient;
    }
    
    public FileManagerService(Service service) : this(service.TcaClient, service)
    {
    }

    public async System.Threading.Tasks.Task<DirectoryListing> GetDirectoryListing(string path = "/")
    {
        var realPath = path[(path.LastIndexOf("/", StringComparison.Ordinal) + 1)..];
        var parentPath = path[..(path.LastIndexOf("/", StringComparison.Ordinal) + 1)];
        var directoryListings = await GetDirectoryListings(parentPath);
        var directoryListing = directoryListings.First(x => x.Path == realPath);
        return directoryListing;
    }

    public async System.Threading.Tasks.Task<IEnumerable<DirectoryListing>> GetDirectoryListings(string path = "/")
    {
        if (string.IsNullOrEmpty(path))
        {
            path = "/";
        }

        path = path.Replace("\\", "/");

        var endpoint = QueryHelpers.AddQueryString(FileManagerEndpoint, nameof(path), path);
        var request = TcaClient.ServicesController.GenerateDefaultRequest(endpoint);
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest<IEnumerable<DirectoryListing>>(request);
        foreach (var directoryListing in executeBaseResponseRequest.Result)
        {
            directoryListing.FileManagerService = this;
        }
        return executeBaseResponseRequest.Result;
    }
    
    public async System.Threading.Tasks.Task<FileInfo> GetFileInfo(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        var endpoint = QueryHelpers.AddQueryString(FileManagerEndpoint, nameof(path), path);
        var request = TcaClient.ServicesController.GenerateDefaultRequest(endpoint);
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest<FileInfo>(request);
        executeBaseResponseRequest.Result.FileManagerService = this;
        return executeBaseResponseRequest.Result;
    }
    
    public async System.Threading.Tasks.Task<bool> Rename(string path, string newName)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (newName == null) throw new ArgumentNullException(nameof(newName));
        var request = TcaClient.ServicesController.GenerateDefaultRequest(HttpMethod.Post, FileManagerEndpoint,nameof(Rename));
        request.Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new(nameof(path), path),
            new(nameof(newName), newName)
        });
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest(request);
        return executeBaseResponseRequest.Success;
    }
    
    public async System.Threading.Tasks.Task<bool> Move(string[] files, string target, bool overwrite = true)
    {
        if (files == null) throw new ArgumentNullException(nameof(files));
        if (target == null) throw new ArgumentNullException(nameof(target));
        var request = TcaClient.ServicesController.GenerateDefaultRequest(HttpMethod.Post, FileManagerEndpoint, nameof(Move));
        request.Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new(nameof(files), JsonConvert.SerializeObject(files)),
            new(nameof(target), target),
            new(nameof(overwrite), overwrite.ToString())
        });
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest(request);
        return executeBaseResponseRequest.Success;
    }
    
    public async System.Threading.Tasks.Task<bool> Copy(string[] files, string target, bool overwrite = true)
    {
        if (files == null) throw new ArgumentNullException(nameof(files));
        if (target == null) throw new ArgumentNullException(nameof(target));
        var request = TcaClient.ServicesController.GenerateDefaultRequest(HttpMethod.Post, FileManagerEndpoint, nameof(Copy));
        request.Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new(nameof(files), JsonConvert.SerializeObject(files)),
            new(nameof(target), target),
            new(nameof(overwrite), overwrite.ToString())
        });
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest(request);
        return executeBaseResponseRequest.Success;
    }
    
    public async System.Threading.Tasks.Task<CompressModel> Compress(string[] files, string target)
    {
        if (files == null) throw new ArgumentNullException(nameof(files));
        if (target == null) throw new ArgumentNullException(nameof(target));
        var request = TcaClient.ServicesController.GenerateDefaultRequest(HttpMethod.Post, FileManagerEndpoint, nameof(Compress));
        request.Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new(nameof(files), JsonConvert.SerializeObject(files)),
            new(nameof(target), target),
        });
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest<CompressModel>(request);
        return executeBaseResponseRequest.Result;
    }
    
    public async System.Threading.Tasks.Task<bool> Extract(string file, string extractPath)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        if (extractPath == null) throw new ArgumentNullException(nameof(extractPath));
        var request = TcaClient.ServicesController.GenerateDefaultRequest(HttpMethod.Post, FileManagerEndpoint, nameof(Compress));
        request.Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new(nameof(file), file),
            new(nameof(extractPath), extractPath),
        });
        var executeBaseResponseRequest = await TcaClient.ServicesController.ExecuteBaseResponseRequest(request);
        return executeBaseResponseRequest.Success;
    }
}