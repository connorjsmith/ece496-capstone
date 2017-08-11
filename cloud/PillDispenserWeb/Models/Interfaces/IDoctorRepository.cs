using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Interfaces
{
    interface IDoctorRepository
    {
        Doctor DoctorById(string doctorId);
        IEnumerable<Doctor> DoctorsByIds(IEnumerable<string> doctorIds);
    }
}
