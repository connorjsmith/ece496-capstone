using PillDispenserWeb.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillDispenserWeb.Models.DataTypes;

namespace PillDispenserWeb.Models.Implementations
{
    public class PatientRepositoryImpl : IPatientRepository
    {
        public Patient PatientById(string patientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> PatientsByDoctorId(string doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> PatientsByPatientIds(IEnumerable<string> patientIds)
        {
            throw new NotImplementedException();
        }
    }
}
