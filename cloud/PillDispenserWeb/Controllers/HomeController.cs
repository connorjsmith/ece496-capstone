using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb
{
    [Route("")]
    public class HomeController : Controller
    {
        #region Homepage Routes

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View("/Views/Home/Index.cshtml");
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
