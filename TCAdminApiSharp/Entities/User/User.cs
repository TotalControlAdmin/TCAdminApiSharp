using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Entities.Generic;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TCAdminApiSharp.Helpers;

namespace TCAdminApiSharp.Entities.User
{
    public class User : ObjectBase, IObjectBaseCrud<User>
    {
        [JsonIgnore] public readonly UsersController UsersController =
            TcaClient.ServiceProvider.GetService<UsersController>() ?? throw new InvalidOperationException();

        [JsonProperty("UserId")] public int UserId { get; set; }

        [JsonProperty("UserName")] public string UserName { get; set; }

        [JsonProperty("RoleId")] public int RoleId { get; set; }

        [JsonProperty("OwnerId")] public int OwnerId { get; set; }

        [JsonProperty("RequiresTwoStepVerification")]
        public bool RequiresTwoStepVerification { get; set; }

        [JsonProperty("FtpRequiresTwoStepVerification")]
        public bool FtpRequiresTwoStepVerification { get; set; }

        [JsonProperty("FtpSkipTwoStepVerificationIfKnownIp")]
        public bool FtpSkipTwoStepVerificationIfKnownIp { get; set; }

        [JsonProperty("TwoStepVerificationSecret")]
        public object TwoStepVerificationSecret { get; set; }

        [JsonProperty("Status")] public int Status { get; set; }

        [JsonProperty("LastLogin")] public DateTime LastLogin { get; set; }

        [JsonProperty("LastLoginUtc")] public DateTime LastLoginUtc { get; set; }

        [JsonProperty("LastLoginIp")] public string LastLoginIp { get; set; }

        [JsonProperty("BillingId")] public string BillingId { get; set; }

        [JsonProperty("BillingStatus")] public int BillingStatus { get; set; }

        [JsonProperty("DemoMode")] public bool DemoMode { get; set; }

        [JsonProperty("IsSubUser")] public bool IsSubUser { get; set; }

        [JsonProperty("SubUserOwnerId")] public int SubUserOwnerId { get; set; }

        [JsonProperty("ExternalId")] public string ExternalId { get; set; }

        [JsonProperty("FirstName")] public string FirstName { get; set; }

        [JsonProperty("LastName")] public string LastName { get; set; }

        [JsonProperty("FullName")] public string FullName { get; set; }

        [JsonProperty("AllAddress")] public string AllAddress { get; set; }

        [JsonProperty("Address1")] public string Address1 { get; set; }

        [JsonProperty("Address2")] public string Address2 { get; set; }

        [JsonProperty("Address3")] public string Address3 { get; set; }

        [JsonProperty("City")] public string City { get; set; }

        [JsonProperty("State")] public string State { get; set; }

        [JsonProperty("Country")] public string Country { get; set; }

        [JsonProperty("Zip")] public string Zip { get; set; }

        [JsonProperty("HomePhone")] public string HomePhone { get; set; }

        [JsonProperty("MobilePhone")] public string MobilePhone { get; set; }

        [JsonProperty("Email1")] public string Email1 { get; set; }

        [JsonProperty("Email2")] public string Email2 { get; set; }

        [JsonProperty("TimeZoneId")] public string TimeZoneId { get; set; }

        [JsonProperty("UserType")] public UserType UserType { get; set; }

        [JsonIgnore] public IList<Service.Service> Services =>
            UsersController.TcaClient.ServicesController.GetServicesByUserId(this.UserId).GetAwaiter().GetResult().Result;

        public async Task<bool> SetPassword(string password)
        {
            var request = UsersController.GenerateDefaultRequest();
            request.Resource += $"setpassword/{UserId}";
            request.Method = Method.POST;
            request.AddParameter("password", password, ParameterType.GetOrPost);
            return (await UsersController.ExecuteBaseResponseRequest(request)).Success;
        }

        public async Task<bool> Update(Action<User> action)
        {
            var service = new User();
            action(service);
            var putJson = JsonConvert.SerializeObject(service, Constants.IgnoreDefaultValues);
            var request = UsersController.GenerateDefaultRequest();
            request.Resource += this.UserId;
            request.Method = Method.PUT;
            request.AddParameter(Constants.JsonContentType, putJson, ParameterType.RequestBody);
            return (await UsersController.ExecuteBaseResponseRequest(request)).Success;
        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> Suspend(bool recursive = true, bool suspendServices = true)
        {
            var request = UsersController.GenerateDefaultRequest();
            request.Resource += $"suspend/{this.UserId}";
            request.Method = Method.POST;
            request.AddParameter(nameof(recursive), recursive, ParameterType.GetOrPost);
            request.AddParameter(nameof(suspendServices), suspendServices, ParameterType.GetOrPost);
            return (await UsersController.ExecuteBaseResponseRequest(request)).Success;
        }
        
        public async Task<bool> Unsuspend(bool recursive = true, bool enableServices = true)
        {
            var request = UsersController.GenerateDefaultRequest();
            request.Resource += $"unsuspend/{this.UserId}";
            request.Method = Method.POST;
            request.AddParameter(nameof(recursive), recursive, ParameterType.GetOrPost);
            request.AddParameter(nameof(enableServices), enableServices, ParameterType.GetOrPost);
            return (await UsersController.ExecuteBaseResponseRequest(request)).Success;
        }
    }
}