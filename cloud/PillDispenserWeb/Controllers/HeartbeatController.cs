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
using PillDispenserWeb.Logic;

namespace PillDispenserWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Heartbeat")]
    public class HeartbeatController : Controller
    {
        IHeartbeat heartbeat;
        public HeartbeatController(IHeartbeat _heartbeat)
        {
            heartbeat = _heartbeat;
        }

        [HttpPost]
        [Route("{deviceid}")]
        public ActionResult HeartbeatFromDevice(string deviceId)
        {
            if (deviceId == null || deviceId.Length == 0)
            {
                return BadRequest();
            }
            heartbeat.AddHeartbeat(deviceId);
            return Json("success");
        }
    }
}