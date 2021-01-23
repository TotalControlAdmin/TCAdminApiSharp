using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog.Events;
using TCAdminApiSharp.Entities.Service;
using TCAdminApiSharp.Exceptions;
using TCAdminApiSharp.Exceptions.API;

namespace TCAdminApiSharp.Tests
{
    public class ServiceTests
    {
        private TcaClient _tcaClient;
        
        [SetUp]
        public void Setup()
        {
            _tcaClient = new TcaClient("https://4a815e4e-aaa8-4351-a948-5ca3f92e5c8b.mock.pstmn.io", "-", TcaClientSettings.Debug);
        }

        [Test]
        public void GetServiceTest()
        {
            var service = _tcaClient.ServicesController.GetService(2);
            Assert.AreEqual(2, service.ServiceId);
        }
        
        [Test]
        public void GetNonExistentServiceTest()
        {
            Assert.Catch<NotFoundException>(() =>
            {
                var _ = _tcaClient.ServicesController.GetService(0);
            });
        }

        [Test]
        public void GetServicesListTest()
        {
            var services = _tcaClient.ServicesController.GetServices();
            Assert.AreEqual(2, services.VirtualCount);
        }
        
        [Test]
        public void GetServiceExceptionTest()
        {
            try
            {
                var _ = _tcaClient.ServicesController.GetService(-1);
            }
            catch (ApiResponseException e)
            {
                Console.WriteLine(e.ErrorResponse);
            }
        }
        
        [Test]
        public void UpdateServiceTest()
        {
            var service = _tcaClient.ServicesController.GetService(1);
            service.Update(x =>
            {
                x.Slots = 12;
                x.Executable = "changed.exe";
            });

            Assert.Pass("Service Updated");
        }

        [Test]
        public void UpdateServiceExceptionTest()
        {
            var service = _tcaClient.ServicesController.GetService(2);
            try
            {
                service.Update(x =>
                {
                    x.Slots = 12;
                    x.Executable = "changed.exe";
                });
            }
            catch (ApiResponseException e)
            {
                var result = ((JObject) e.ErrorResponse.Result).ToObject<Exception>();
                if (result?.Source == "TCAdmin.Web.MVC") //todo Update this to the specific Update Service exception type.
                {
                    Assert.Pass("Correct exception returned");
                }
                Console.WriteLine(result);
            }
        
            Assert.Fail("No exception");
        }
        
        // [Test]
        // public void ServiceCreateJsonTest()
        // {
        //     var serviceBuilder = new ServiceBuilder()
        //         .WithAutomaticPort(true)
        //         .WithGameId(12)
        //         .WithBranded(true)
        //         .WithDatacenterId(1)
        //         .WithVariables(new Dictionary<string, object>()
        //         {
        //             {"test", 123}
        //         })
        //         .WithSlots(100)
        //         .WithBillingId("100Not4Me");
        //     _tcaClient.ServicesController.CreateService(serviceBuilder);
        // }
    }
}