using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using TCAdminApiSharp.Entities.Generic;
using TCAdminApiSharp.Helpers;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace TCAdminApiSharp.Entities.Service
{
    public class ServiceBuilder
    {
        private readonly ILogger _logger = Log.ForContext<ServiceBuilder>();
        private readonly Service _service;
        private readonly Dictionary<string, object> _extraData = new();

        public ServiceBuilder()
        {
            _service = new Service();
            WithStartup(ServiceStartup.Automatic);
            WithPriority(ProcessPriorityClass.Normal);
        }

        public ServiceBuilder WithServerId(int id)
        {
            _service.ServerId = id;
            return this;
        }

        public ServiceBuilder WithDatacenterId(int id)
        {
            _extraData["DatacenterId"] = id;
            return this;
        }

        public ServiceBuilder WithUserId(int id)
        {
            _service.UserId = id;
            return this;
        }
        
        public ServiceBuilder WithUser(User.User user)
        {
            _service.UserId = user.UserId;
            return this;
        }

        public ServiceBuilder WithGameId(int id)
        {
            _service.GameId = id;
            return this;
        }

        public ServiceBuilder WithAutomaticPort(bool auto)
        {
            _extraData["AutomaticPort"] = auto;
            return this;
        }

        public ServiceBuilder WithPort(int port)
        {
            _service.GamePort = port;
            return this;
        }

        public ServiceBuilder WithSlots(int slots)
        {
            _service.Slots = slots;
            return this;
        }

        public ServiceBuilder WithPrivate(bool priv)
        {
            _service.Private = priv;
            return this;
        }

        public ServiceBuilder WithVariables(Dictionary<string, object> variables)
        {
            _service.Variables = new TcaXmlField(variables);
            return this;
        }

        public ServiceBuilder WithStartup(ServiceStartup startup)
        {
            _service.Startup = startup;
            return this;
        }

        public ServiceBuilder WithPriority(ProcessPriorityClass priority)
        {
            _service.Priority = (int)priority;
            return this;
        }

        public ServiceBuilder WithAffinity(int affinity)
        {
            _service.Affinity = affinity;
            return this;
        }

        public ServiceBuilder WithNumaNode(int numaNode)
        {
            _service.NumaNode = numaNode;
            return this;
        }

        public ServiceBuilder WithBranded(bool branded)
        {
            _service.Branded = branded;
            return this;
        }

        public ServiceBuilder WithStartAfterCreated(bool startAfterCreation)
        {
            _extraData["StartAfterCreated"] = startAfterCreation;
            return this;
        }

        public ServiceBuilder WithScheduleDelete(bool scheduleDelete)
        {
            _extraData["ScheduleDelete"] = scheduleDelete;
            return this;
        }

        public ServiceBuilder WithDeleteTimeUtc(DateTime deletionTime)
        {
            _extraData["DeleteTimeUtc"] = deletionTime;
            return this;
        }

        public ServiceBuilder WithDeleteOwner(bool deleteOwner)
        {
            _extraData["DeleteOwner"] = deleteOwner;
            return this;
        }

        public ServiceBuilder WithUserPackageId(int userPackageId)
        {
            _service.UserPackageId = userPackageId;
            return this;
        }

        public ServiceBuilder WithGameSwitchingAllowedGames(string[] gameIds)
        {
            _service.GameSwitchingAllowedGames = gameIds;
            return this;
        }

        public ServiceBuilder WithBillingId(string billingId)
        {
            _service.BillingId = billingId;
            return this;
        }

        public ServiceBuilder WithCpuLimit(int cpuLimit)
        {
            _service.CpuLimit = cpuLimit;
            return this;
        }

        public ServiceBuilder WithVirtualMemoryLimitMb(int mb)
        {
            _extraData["VirtualMemoryLimitMB"] = mb;
            return this;
        }

        public ServiceBuilder WithMemoryLimitMb(int mb)
        {
            _service.MemoryLimitMB = mb;
            return this;
        }

        internal string GenerateRequestBody()
        {
            var jo = JObject.FromObject(this._service, JsonSerializer.Create(Constants.IgnoreDefaultValues));
            foreach (var keyValuePair in _extraData)
            {
                jo.Add(keyValuePair.Key, new JValue(keyValuePair.Value));
            }
            _logger.Debug(jo.ToString());

            return jo.ToString();
        }
    }
}