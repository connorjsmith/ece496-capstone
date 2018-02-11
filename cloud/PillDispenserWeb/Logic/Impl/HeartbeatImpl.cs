using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PillDispenserWeb.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Logic.Impl
{
    public class HeartbeatImpl : IHeartbeat
    {
        public SortedSet<string> LastMissingDeviceIds = new SortedSet<string>();
        public SortedSet<string> LastIterationDeviceIds = new SortedSet<string>();
        public SortedSet<string> CurrentIterationDeviceIds = new SortedSet<string>();
        private IRecurringJobManager heartbeatTaskManager;
        private HeartbeatConfig config;
        public HeartbeatImpl(IOptions<HeartbeatConfig> _config, IRecurringJobManager _heartbeatTaskManager = null) {
            heartbeatTaskManager = _heartbeatTaskManager ?? new RecurringJobManager();
            config = _config.Value;

            heartbeatTaskManager.AddOrUpdate(
                "heartbeat",
                Job.FromExpression(() => HeartbeatTask()),
                Cron.MinuteInterval(config.IntervalMinutes)
            );
        }

        public void AddHeartbeat(string deviceId)
        {
            lock (CurrentIterationDeviceIds)
            {
                CurrentIterationDeviceIds.Add(deviceId);
            }
        }

        public void HeartbeatTask()
        {
            lock (CurrentIterationDeviceIds)
            {
                // Missing twice in a row = lastMissing - currentIteration
                var missedTwo = new SortedSet<string>(LastMissingDeviceIds.Except(CurrentIterationDeviceIds));
                // TODO: notify all the missedTwo patients

                // first time missing = lastIteration - currentIteration
                LastMissingDeviceIds = new SortedSet<string>(LastIterationDeviceIds.Except(CurrentIterationDeviceIds));

                // newly added = currentIteration - lastIteration
                // TODO: necessary?

                // lastIteration = currentIteration
                LastIterationDeviceIds = new SortedSet<string>(CurrentIterationDeviceIds);
                CurrentIterationDeviceIds.Clear();
            }
        }
    }
}
