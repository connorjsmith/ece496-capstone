using PillDispenserWeb.Models.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Patient
    {
        public long PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public List<PatientDoctor> Doctors { get; set; }
        public List<Prescription> Prescriptions { get; set; }
    }
}
