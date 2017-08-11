using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PillDispenserWeb.Models.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb
{
    [Route("")]
    public class HomeController : Controller
    {

        private IDoctorRepository doctors;

        #region Setup and Teardown
        public HomeController(IDoctorRepository _doctors)
        {
            // Populated via dependency injection in Startup.cs
            doctors = _doctors;
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

        #endregion Example Secondary Route
    }
}
