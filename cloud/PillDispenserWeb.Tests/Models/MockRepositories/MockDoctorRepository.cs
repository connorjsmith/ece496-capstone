using GenFu;
using GenFu.ValueGenerators.People;
using PillDispenserWeb.Models.DataTypes;
using PillDispenserWeb.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PillDispenserWeb.Tests.Models.MockRepositories
{
    public class MockDoctorRepository : IDoctorRepository
    {

        public IEnumerable<Doctor> doctors = new List<Doctor>();
        public MockDoctorRepository (int NumMockDoctors = 40)
        {
            var _doctors = new List<Doctor>(NumMockDoctors);
            for (int i = 0; i < NumMockDoctors; ++i)
            {
                var doc = new Doctor();
                doc.FirstName = "MockDoctorFirstName" + i;
                doc.LastName = "MockDoctorLastName" + i;
            }
            doctors = _doctors;
        }

        public Doctor DoctorById(string doctorId)
        {
            return doctors.First(d => d.DoctorId == doctorId);
        }

        public IEnumerable<Doctor> DoctorsByIds(IEnumerable<string> doctorIds)
        {
            return doctors.Where(d => doctorIds.Contains(d.DoctorId));
        }
    }
}
