using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Configuration;

namespace PillDispenserWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Heartbeat")]
    public class HeartbeatController : Controller
    {
        public SortedSet<string> LastMissingDeviceIds;
        public SortedSet<string> LastIterationDeviceIds;
        public SortedSet<string> CurrentIterationDeviceIds;
        private RecurringJobManager heartbeatTaskManager;

        public HeartbeatController(
            IConfiguration Configuration,
            IRecurringJobManager heartbeatTaskManager = null,
            SortedSet<string> lastMissing = null, SortedSet<string> lastIteration = null, SortedSet<string> currentIteration = null)
        {
            heartbeatTaskManager = heartbeatTaskManager ?? new RecurringJobManager();
            LastMissingDeviceIds = lastMissing ?? new SortedSet<string>();
            LastIterationDeviceIds = lastIteration ?? new SortedSet<string>();
            CurrentIterationDeviceIds = currentIteration ?? new SortedSet<string>();

            int heartbeatIntervalMinutes = Int32.Parse(Configuration.GetSection("HeartbeatConfig")["MinuteInterval"]);
            heartbeatTaskManager.AddOrUpdate(
                "heartbeat", 
                Job.FromExpression(() => HeartbeatTask()), 
                Cron.MinuteInterval(heartbeatIntervalMinutes)
            );
        }

        [NonAction]
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

        [NonAction]
        public ActionResult HeartbeatFromDevice(string deviceId)
        {
            if (deviceId == null || deviceId.Length == 0)
            {
                return BadRequest();
            }

            lock (CurrentIterationDeviceIds)
            {
                CurrentIterationDeviceIds.Add(deviceId);
            }
            return Json("success");
        }

        [HttpPost]
        [Route("")]
        public ActionResult HeartbeatFromDevice()
        {
            // TODO some sort of secret key to better authenticate these endpoints?
            string deviceId = Request.Form["deviceId"];
            return HeartbeatFromDevice(deviceId);
        }
    }
}