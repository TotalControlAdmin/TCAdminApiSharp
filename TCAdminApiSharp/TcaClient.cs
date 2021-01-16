using System;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Serilog;
using Serilog.Events;
using TCAdminApiSharp.Controllers;
// ReSharper disable NotAccessedField.Global

namespace TCAdminApiSharp
{
    public class TcaClient
    {
        public readonly string Host;
        private readonly string _apiKey;
        internal static ServiceProvider ServiceProvider;
        internal readonly RestClient RestClient;
        public readonly ServicesController ServicesController;
        public readonly UsersController UsersController;
        public readonly TasksController TasksController;

        public TcaClient(string host, string apiKey, LogEventLevel minimumLogLevel = LogEventLevel.Information)
        {
            SetupDefaultLogger(minimumLogLevel);
            CreateHostBuilder();
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Parameter is null/empty", nameof(host));
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("Parameter is null/empty", nameof(apiKey));
            }

            Host = host;
            _apiKey = apiKey;

            RestClient = new RestClient(Host);
            RestClient.AddDefaultHeader("api_key", apiKey);
            
            ServicesController = ServiceProvider.GetService<ServicesController>() ?? throw new InvalidOperationException();
            UsersController = ServiceProvider.GetService<UsersController>() ?? throw new InvalidOperationException();
            TasksController = ServiceProvider.GetService<TasksController>() ?? throw new InvalidOperationException();
        }

        internal int GetTokenUserId()
        {
            return int.Parse(this._apiKey.Split('#')[0]);
        }

        private void CreateHostBuilder()
        {
            ServiceProvider = new ServiceCollection()
                .AddTransient(_ => this)
                .AddScoped<ServicesController>()
                .AddScoped<UsersController>()
                .AddScoped<TasksController>()
                .BuildServiceProvider();
        }

        private static void SetupDefaultLogger(LogEventLevel logEventLevel)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logEventLevel)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}