using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Relations
{
    public class PatientDoctor
    {
        public long PatientId { get; set; }
        public Patient Patient { get; set; }
        public long DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
