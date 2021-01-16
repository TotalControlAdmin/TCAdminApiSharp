using System;
using Newtonsoft.Json;
using TCAdminApiSharp.Controllers;
using TCAdminApiSharp.Entities.Generic;
using Microsoft.Extensions.DependencyInjection;

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

        public void Update(Action<User> action)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}