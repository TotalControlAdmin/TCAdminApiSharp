using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TCAdminApiSharp.Exceptions.API;
using TCAdminApiSharp.Exceptions.Services;

namespace TCAdminApiSharp.Tests
{
    public class ServiceTests
    {
        private TcaClient _tcaClient;
        
        [SetUp]
        public void Setup()
        {
            _tcaClient = new TcaClient("https://4a815e4e-aaa8-4351-a948-5ca3f92e5c8b.mock.pstmn.io", "-");
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
            Assert.Catch<ServiceNotFoundException>(() =>
            {
                var _ = _tcaClient.ServicesController.GetService(0);
            });
        }
        
        [Test]
        public void ServiceCustomVariablesParseCheck()
        {
            var service = _tcaClient.ServicesController.GetService(1);
            Assert.AreEqual("minecraft_server2.jar", service.Variables.Values["Jar"]);
        }
        
        [Test]
        public void ServiceAppDataParseCheck()
        {
            var service = _tcaClient.ServicesController.GetService(1);
            Assert.AreEqual(false, (bool)service.AppData.Values["__TCA:PROCESSING"]);
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
    }
}