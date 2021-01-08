using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using TCAdminApiSharp.Controllers;

namespace TCAdminApiSharp
{
    public class TcaClient
    {
        internal static ServiceProvider ServiceProvider;
        public readonly string Host;
        internal readonly RestClient RestClient;
        public readonly ServicesController ServicesController;
        public readonly UsersController UsersController;

        public TcaClient(string host, string apiKey)
        {
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

            RestClient = new RestClient(Host);
            RestClient.AddDefaultHeader("api_key", apiKey);
            
            ServicesController = ServiceProvider.GetService<ServicesController>() ?? throw new InvalidOperationException();
        }

        private void CreateHostBuilder()
        {
            ServiceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddTransient(_ => this)
                .AddScoped<ServicesController>()
                .BuildServiceProvider();
        }
    }
}