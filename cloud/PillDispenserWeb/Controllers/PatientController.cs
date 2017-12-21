using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.Identity.Policies;
using Microsoft.Extensions.Primitives;

namespace PillDispenserWeb.Controllers
{
    [Route("Patient")]
    [Authorize]
    public class PatientController : Controller
    {

        private readonly IAuthorizationService authorizationService;
        private readonly AppDataContext appDataContext;
        public PatientController(IAuthorizationService _authorizationService, AppDataContext _appDataContext)
        {
            authorizationService = _authorizationService;
            appDataContext = _appDataContext;
        }
        [HttpGet("{patientId}/Details")]
        public async Task<IActionResult> Details(long patientId)
        {
            var patient = appDataContext.Patients.Find(patientId);
            // TODO register Handler with service in Startup.cs
            var authorizationResult = await authorizationService.AuthorizeAsync(User, patient, new List<IAuthorizationRequirement> { new DoctorForPatientRequirement() });
            if (!authorizationResult.Succeeded)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpGet("{patientId}/Details/Doses/json")]
        public async Task<JsonResult> DoseDetails(long patientId)
        {
            var patient = appDataContext.Patients.Find(patientId);
            // TODO register Handler with service in Startup.cs
            var authorizationResult = await authorizationService.AuthorizeAsync(User, patient, new List<IAuthorizationRequirement> { new DoctorForPatientRequirement() });
            if (!authorizationResult.Succeeded)
            {
                return Json("Not authorized");
            }
            return Json("replace me");
        }

        public class DoseSummaryResult
        {
            public string medicationName { get; set; }
            public int numDosesTaken { get; set; }
            public int numDosesMissed { get; set; }
        }
        /* Given a date range, Returns
         * [
         *     {
         *         medicationName: <string>,
         *         numDosesTaken: <int>,
         *         numDosesMissed: <int>
         *     },
         *     ...
         * ]
         */
        [HttpGet("{patientId}/Details/DosesSummary/json")]
        public async Task<JsonResult> DoseSummary(long patientId, [FromQuery]DateTimeOffset? StartDate, [FromQuery]DateTimeOffset? EndDate)
        {
            var patient = appDataContext.Patients.Find(patientId);
            #region Authentication and parameter verification
            // TODO register Handler with service in Startup.cs
            var authorizationResult = await authorizationService.AuthorizeAsync(User, patient, new List<IAuthorizationRequirement> { new DoctorForPatientRequirement() });
            if (!authorizationResult.Succeeded)
            {
                return Json("Not authorized");
            }
            else if (!StartDate.HasValue)
            {
                return Json("StartDate required");
            }
            else if (!EndDate.HasValue)
            {
                return Json("EndDate required");
            }
            #endregion

            var summaryList = new List<DoseSummaryResult>();
            foreach (var prescription in patient.Prescriptions)
            {
                int numberOfExpectedDoses = prescription.Recurrences.Sum(r => r.GetExpectedNumberOfDoses(StartDate.Value, EndDate.Value));
                int numberOfTakenDoses = prescription.Recurrences.Sum(r => r.Doses.Count());
                summaryList.Add(new DoseSummaryResult {
                    medicationName = prescription.Medication.PlaintextName,
                    numDosesMissed =numberOfExpectedDoses-numberOfTakenDoses,
                    numDosesTaken =numberOfTakenDoses
                });
            }
            return Json(summaryList);
        }
    }
}