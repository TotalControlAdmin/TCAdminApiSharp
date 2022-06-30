using System;
using System.Net.Http;
using Serilog;
using Serilog.Events;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Helpers;

namespace TCAdminApiSharp;

public class TcaClient : IDisposable
{
    public readonly string Host;
    private readonly string _apiKey;
    public readonly ServicesController ServicesController;
    public readonly ServersController ServersController;
    public readonly UsersController UsersController;
    public readonly TasksController TasksController;
    public readonly TcaClientSettings Settings;
    internal readonly HttpClient HttpClient;

    public TcaClient(string host, string apiKey, TcaClientSettings? clientSettings = null)
    {
        clientSettings ??= TcaClientSettings.Default;
        SetupDefaultLogger(clientSettings.MinimumLogLevel);
        if (string.IsNullOrEmpty(host)) throw new ArgumentException("Parameter is null/empty", nameof(host));
        if (string.IsNullOrEmpty(apiKey)) throw new ArgumentException("Parameter is null/empty", nameof(apiKey));

        Settings = clientSettings;
        Host = host;
        _apiKey = apiKey;

        HttpClient = new HttpClient();
        HttpClient.BaseAddress = new Uri(Host);
        HttpClient.DefaultRequestHeaders.Add("api_key", _apiKey);
        HttpClient.DefaultRequestHeaders.Add("accept", Constants.JsonContentType);

        ServicesController = new ServicesController(this);
        ServersController = new ServersController(this);
        UsersController = new UsersController(this);
        TasksController = new TasksController(this);
    }

    internal int GetTokenUserId()
    {
        return int.Parse(_apiKey.Split('#')[0]);
    }

    private static void SetupDefaultLogger(LogEventLevel logEventLevel)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(logEventLevel)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}