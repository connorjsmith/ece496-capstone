using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillDispenserWeb.Logic;

namespace PillDispenserWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Reporting")]
    public class ReportingController : Controller
    {

        private IHeartbeat heartbeat;
        public ReportingController(IHeartbeat _heartbeat)
        {
            heartbeat = _heartbeat;
        }

        public JsonResult Taken()
        {
            string deviceId = Request.Form["deviceId"];
            // TODO: authenticate
            // TODO: Insert taken record into database
            
            // add heartbeat
            heartbeat.AddHeartbeat(deviceId);
            return Json("success");
            
        }
    }
}