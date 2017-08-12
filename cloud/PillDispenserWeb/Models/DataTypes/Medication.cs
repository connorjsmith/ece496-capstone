using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Medication
    {
        public string MedicationId { get; set; }
        public string PlaintextName { get; set; }
        public float DosageInMg { get; set; }
    }
}
