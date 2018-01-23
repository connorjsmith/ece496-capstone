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


        [NonAction]
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

        [HttpPost("Taken/{deviceId}/{medicationId}/{recurrenceId}/{time}")]
        public JsonResult Taken(string deviceId, string medicationId, string recurrenceId, DateTimeOffset time)
        {
            return RecordMedicationEvent(deviceId, medicationId, recurrenceId, time, true);
        }

        [HttpPost("Missed/{deviceId}/{medicationId}/{recurrenceId}/{time}")]
        public JsonResult Missed(string deviceId, string medicationId, string recurrenceId, DateTimeOffset time)
        {
            return RecordMedicationEvent(deviceId, medicationId, recurrenceId, time, false);
        }
    }
}