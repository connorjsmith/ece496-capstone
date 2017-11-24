using PillDispenserWeb.Models.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Dose
    {
        public string DoseId { get; set; }
        public Prescription.Recurrence AssociatedRecurrence { get; set; }
        public DateTimeOffset TimeTaken { get; set; }
    }
}
