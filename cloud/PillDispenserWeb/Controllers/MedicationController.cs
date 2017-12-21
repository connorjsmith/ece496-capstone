using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PillDispenserWeb.Models;
using PillDispenserWeb.Models.DataTypes;
using Microsoft.AspNetCore.Authorization;

namespace PillDispenserWeb.Controllers
{
    [Route("Medication")]
    [Authorize]
    public class MedicationController : Controller
    {
        private readonly AppDataContext _context;

        public MedicationController(AppDataContext context)
        {
            _context = context;
        }

        // GET: Medication/
        [HttpGet("")]
        public async Task<IActionResult> Search()
        {
            // TODO show view for searching medications
            var allMeds = _context.Medications.Include(m=>m.SideEffects).ToList();
            // var allMeds = _context.Medications.GroupJoin(
                // _context.SideEffects,
                // m=>m.MedicationId,
                // s=>s.AssociatedMedication,
                // (m, s) => new {Medication = m, SideEffects = s}
                // ).Select()
            return View(allMeds);
        }

        // GET: Medication/5/Details
        [HttpGet("{id}/Details")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await _context.Medications
                .SingleOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medication/5/Details/json
        [HttpGet("{id}/Details/json")]
        public async Task<JsonResult> DetailsJson(string id)
        {
            if (id == null)
            {
                return Json(null);
            }

            var medication = await _context.Medications
                .SingleOrDefaultAsync(m => m.MedicationId == id);
            if (medication == null)
            {
                return Json(null);
            }

            return Json(medication);
        }

        private bool MedicationExists(string id)
        {
            return _context.Medications.Any(e => e.MedicationId == id);
        }
    }
}
