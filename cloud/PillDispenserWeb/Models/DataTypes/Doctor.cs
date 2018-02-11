using PillDispenserWeb.Models.Relations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Doctor
    {
        public long DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get { return $"{FirstName} {LastName}"; }
        }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public IEnumerable<PatientDoctor> Patients { get; set; }

    }
}
