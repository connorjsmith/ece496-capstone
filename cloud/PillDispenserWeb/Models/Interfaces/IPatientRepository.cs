using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Interfaces
{
    public interface IPatientRepository
    {
        Patient PatientById(string patientId);
        IEnumerable<Patient> PatientsByPatientIds(IEnumerable<string> patientIds);
        IEnumerable<Patient> PatientsByDoctorId(string doctorId);
    }
}
