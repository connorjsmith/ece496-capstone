using PillDispenserWeb.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Dose
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DoseId { get; set; }
        public Prescription.Recurrence AssociatedRecurrence { get; set; }
        public DateTimeOffset TimeTaken { get; set; }
        public bool wasOnTime { get; set; }
    }
}
