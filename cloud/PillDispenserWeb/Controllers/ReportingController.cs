using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillDispenserWeb.Logic;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.DataTypes;
using static PillDispenserWeb.Models.Relations.Prescription;

namespace PillDispenserWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Reporting")]
    public class ReportingController : Controller
    {

        private IHeartbeat heartbeat;
        private AppDataContext dataContext;
        public ReportingController(IHeartbeat _heartbeat, AppDataContext _appDataContext)
        {
            heartbeat = _heartbeat;
            dataContext = _appDataContext;
        }

        public JsonResult RecordMedicationEvent(string deviceId, string medicationId, string recurrenceId, DateTimeOffset time, bool wasTakenOnTime)
        {
            // TODO: authenticate
            var associatedRecurrence = dataContext.Recurrence.First(x => x.RecurrenceId == recurrenceId);
            if (associatedRecurrence == null)
            {
                return Json("failed");
            }

            // add heartbeat
            heartbeat.AddHeartbeat(deviceId);

            // TODO: ensure the patient for the device was prescribed that medicationId and has a matching recurrenceId for it
            // TODO: Add a Dose record, associated it with the recurrence
            var dose = new Dose { AssociatedRecurrence = associatedRecurrence, TimeTaken = time, wasOnTime = wasTakenOnTime };
            associatedRecurrence.Doses.Add(dose);
            dataContext.SaveChanges();
            
            return Json("success");
        }

        [HttpPost("Taken")]
        public JsonResult Taken()
        {
            string deviceId = Request.Form["deviceId"];
            string medicationId = Request.Form["medicationId"];
            string recurrenceId = Request.Form["recurrenceId"];
            DateTimeOffset time = DateTimeOffset.Parse(Request.Form["time"]); // TODO: assert the device sends proper formatted string
            return RecordMedicationEvent(deviceId, medicationId, recurrenceId, time, true);
        }

        [HttpPost("Missed")]
        public JsonResult Missed()
        {
            //string deviceId = Request.Form["deviceId"];
            //string medicationId = Request.Form["medicationId"];
            //string recurrenceId = Request.Form["recurrenceId"];
            //DateTimeOffset time = DateTimeOffset.Parse(Request.Form["time"]); // TODO: assert the device sends proper formatted string
            string deviceId = "1";
            string medicationId = "1";
            string recurrenceId = "1";
            DateTimeOffset time = DateTimeOffset.MaxValue;
            return RecordMedicationEvent(deviceId, medicationId, recurrenceId, time, false);
        }
    }
}