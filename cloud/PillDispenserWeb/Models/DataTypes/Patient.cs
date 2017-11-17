using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string[] CaregiverIds { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
