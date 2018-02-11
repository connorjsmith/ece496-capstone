using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PillDispenserWeb.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: /<controller>/
        [Route("/Error/404")]
        public IActionResult PageNotFound()
        {
            return View("/Views/Error/NotFound.cshtml");
        }
    }
}
