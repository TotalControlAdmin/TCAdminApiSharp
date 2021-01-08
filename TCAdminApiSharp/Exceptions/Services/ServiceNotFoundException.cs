using System;

namespace TCAdminApiSharp.Exceptions.Services
{
    public class ServiceNotFoundException : Exception
    {
        public readonly int ServiceId;

        public ServiceNotFoundException(int serviceId, Exception innerException) : base("The service with ID: " + serviceId + " does not exist", innerException)
        {
            ServiceId = serviceId;
        }
    }
}