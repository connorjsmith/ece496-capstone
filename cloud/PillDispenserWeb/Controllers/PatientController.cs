﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.Identity.Policies;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{patientId}/CurrentSchedule")]
        [Produces("application/json")]
        [AllowAnonymous]
        public JsonResult CurrentSchedule(long patientId)
        {
            var patient = appDataContext.Patients
                .Include(pat => pat.Prescriptions)
                    .ThenInclude(per => per.Recurrences)
                .FirstOrDefault(pat => pat.PatientId == patientId);

            var now = DateTimeOffset.Now;

            var currSchedule = patient?.Prescriptions
                .Where(per => per.Recurrences.Any(r => r.End > now && r.Start <= now));

            return Json(currSchedule);
        }
    }
}