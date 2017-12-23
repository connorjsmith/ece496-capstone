using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PillDispenserWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Heartbeat")]
    public class HeartbeatController : Controller
    {
        public SortedSet<string> LastMissingDeviceIds;
        public SortedSet<string> LastIterationDeviceIds;
        public SortedSet<string> CurrentIterationDeviceIds;

        public HeartbeatController(SortedSet<string> lastMissing = null, SortedSet<string> lastIteration = null, SortedSet<string> currentIteration = null)
        {
            LastMissingDeviceIds = lastMissing ?? new SortedSet<string>();
            LastIterationDeviceIds = lastIteration ?? new SortedSet<string>();
            CurrentIterationDeviceIds = currentIteration ?? new SortedSet<string>();
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

        [HttpPost]
        [Route("/")]
        public JsonResult HeartbeatFromDevice(string deviceId)
        {
            // TODO some sort of secret key to better authenticate these endpoints?
            lock (CurrentIterationDeviceIds)
            {
                CurrentIterationDeviceIds.Add(deviceId);
            }
            return Json("success");
        }
    }
}