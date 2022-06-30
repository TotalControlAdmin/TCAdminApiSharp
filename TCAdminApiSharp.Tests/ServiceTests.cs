using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Exceptions.API;

namespace TCAdminApiSharp.Tests;

public class ServiceTests
{
    private TcaClient _tcaClient;
        
    [SetUp]
    public void Setup()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddJsonFile("appsettings.local.json", true).Build().GetSection("TCAdmin");
        var activeProfile = config["ActiveProfile"];
        var profileSection = config.GetSection("Profiles").GetSection(activeProfile);
        var host = profileSection["Host"];
        var key = profileSection["Key"];
        _tcaClient = new TcaClient(host, key, TcaClientSettings.Debug);
    }

    [Test]
    public async Task GetServiceTest()
    {
        var service = await _tcaClient.ServicesController.GetService(1);
        Assert.AreEqual(1, service.ServiceId);
    }
    
    [Test]
    public async Task ServiceStart()
    {
        var service = await _tcaClient.ServicesController.GetService(1);
        await service.Start();
        service = await _tcaClient.ServicesController.GetService(1);
        Assert.AreEqual(ServiceStatus.Running, service.ServiceStatus);
    }

    [Test]
    public async Task GetServicesListTest()
    {
        var services = await _tcaClient.ServicesController.GetServices();
        Assert.AreEqual(2, services.Count);
    }
        
    [Test]
    public async Task GetServiceExceptionTest()
    {
        Assert.ThrowsAsync<ApiResponseException>(async () =>
        {
            var service = await _tcaClient.ServicesController.GetService(-1);
            Console.WriteLine(service.ServiceId);
        });
    }
    
    [Test]
    public async Task GetServiceFileManagerService()
    {
        var service = await _tcaClient.ServicesController.GetService(3);
        Assert.NotNull(service.FileManagerService);
    }
    
    [Test]
    public async Task FileManagerGetFileInfo()
    {
        var service = await _tcaClient.ServicesController.GetService(3);
        var fileInfo = await service.FileManagerService.GetFileInfo("/cfg/server.cfg");
        Assert.AreEqual("server.cfg", fileInfo.Name);
        Assert.AreEqual(".cfg", fileInfo.Extension);
    }
    
    [Test]
    public async Task FileManagerCopyFileInfo()
    {
        var service = await _tcaClient.ServicesController.GetService(3);
        var fileInfo = await service.FileManagerService.GetFileInfo("/cfg/server.cfg");
        var copy = await fileInfo.Copy("/cfg2");
        Assert.IsTrue(copy);
    }
    
    [Test]
    public async Task FileManagerCompressDirectory()
    {
        var service = await _tcaClient.ServicesController.GetService(3);
        var directoryListing = await service.FileManagerService.GetDirectoryListing("/cfg");
        var compressModel = await directoryListing.Compress("/cfg2");
        Assert.IsNotNull(compressModel.Zip);
    }
        
    [Test]
    public async Task UpdateServiceTest()
    {
        var service = await _tcaClient.ServicesController.GetService(1);
        Assert.AreEqual(1, service.ServiceId);
        await service.Update(x =>
        {
            x.Slots = 5;
            // x.Executable = "changed.exe";
        });
    }

    // [Test]
    // public async Task UpdateServiceExceptionTest()
    // {
    //     var service = await _tcaClient.ServicesController.GetService(2);
    //     try
    //     {
    //         await service.Update(x =>
    //         {
    //             x.Slots = 12;
    //             x.Executable = "changed.exe";
    //         });
    //     }
    //     catch (ApiResponseException e)
    //     {
    //         var result = (e.HttpResponseMessage).ToObject<Exception>();
    //         if (result?.Source == "TCAdmin.Web.MVC") //todo Update this to the specific Update Service exception type.
    //         {
    //             Assert.Pass("Correct exception returned");
    //         }
    //         Console.WriteLine(result);
    //     }
    //
    //     Assert.Fail("No exception");
    // }
        
    // [Test]
    // public async Task ServiceCreateJsonTest()
    // {
    //     var serviceBuilder = new ServiceBuilder()
    //         .WithAutomaticPort(true)
    //         .WithGameId(67)
    //         .WithBranded(true)
    //         .WithDatacenterId(1)
    //         // .WithVariables(new TcaXmlField()
    //         // {
    //         //     {"test", 123}
    //         // })
    //         .WithSlots(100)
    //         .WithBillingId("100Not4Me");
    //     try
    //     {
    //         var service = await _tcaClient.ServicesController.CreateService(serviceBuilder);
    //         Console.WriteLine("Sv: " + service);
    //     }
    //     catch (ApiResponseException e)
    //     {
    //         Console.WriteLine(e.ExceptionResponse.Success);
    //         Console.WriteLine(e.ExceptionResponse.Message);
    //         throw;
    //     }
    // }
}