using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.DataTypes
{
    public class Patient
    {
        string Name;
        string PhoneNumber;
        string EmailAddress;
        string[] CaregiverIds;
        string[] DoctorIds;
    }
}
