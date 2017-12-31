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

        [NonAction]
        public ActionResult HeartbeatFromDevice(string deviceId)
        {
            if (deviceId == null || deviceId.Length == 0)
            {
                return BadRequest();
            }
            heartbeat.AddHeartbeat(deviceId);
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