using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TCAdminApiSharp.Tests;

public class ServerTests
{
    private TcaClient _tcaClient;
        
    [SetUp]
    public void Setup()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.local.json", true)
            .Build().GetSection("TCAdmin");
        var activeProfile = config["ActiveProfile"];
        var profileSection = config.GetSection("Profiles").GetSection(activeProfile);
        var host = profileSection["Host"];
        var key = profileSection["Key"];
        _tcaClient = new TcaClient(host, key, TcaClientSettings.Debug);
    }

    [Test]
    public async Task GetServerTest()
    {
        var server = await _tcaClient.ServersController.GetServer(1);
        Assert.AreEqual("Master", server.Name);
    }
    
    // [Test]
    // public async Task GetServersTest()
    // {
    //     var server = await _tcaClient.ServersController.GetServers();
    //     Assert.IsTrue(server.Success);
    //     Assert.AreEqual(1, server.Result.Count);
    //     Assert.AreEqual("Master", server.Result[0].Name);
    // }
}