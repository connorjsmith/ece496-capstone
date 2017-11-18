using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PillDispenserWeb.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb
{
    [Route("")]
    public class HomeController : Controller
    {

        private AppDataContext appDataContext;
        #region Setup and Teardown
        public HomeController(AppDataContext _appDataContext)
        {
            // Populated via dependency injection in Startup.cs
            appDataContext = _appDataContext;
        }

        #endregion

        #region Homepage Routes

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View("/Views/Home/Index.cshtml");
        }

        [HttpGet("antd-exploration")]
        public async Task<IActionResult> Antd()
        {
            return View("/Views/Frontend Explorations/antd.cshtml");
        }

        #endregion Homepage Routes

        #region Example Secondary Route

        [HttpGet("this/is/a/path")]
        public async Task<IActionResult> Other()
        {
            return View("/Views/Other/Other.cshtml");
        }

        [HttpGet("doctors")]
        public JsonResult GetAllDoctors()
        {
            var res = appDataContext.Doctors
                .Include(d => d.Patients) // Join the PatientDoctor table
                .ThenInclude(pd => pd.Patient) // Join the Patient table
                // At this point we have something like
                // Doctor.FullName PatientDoctor.DoctorId PatientDoctor.PatientId Patient.FirstName
                // Select this down to a new object with just Name = Doctor.FullName and Patients=[Patient.FirstName1, Patient.FirstName2...]
                .Select(a => new { Name = a.FullName, Patients = a.Patients.Select(p => p.Patient.FirstName).ToList() })
                .ToList();
            return Json(res);
        }

        #endregion Example Secondary Route
    }
}
