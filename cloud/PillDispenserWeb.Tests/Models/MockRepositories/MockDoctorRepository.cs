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
            A.Configure<Doctor>()
                .Fill(d => d.DoctorId, () => Guid.NewGuid().ToString())
                .Fill(d => d.PhoneNumber).AsPhoneNumber()
                .Fill(d => d.FirstName).AsFirstName()
                .Fill(d => d.LastName).AsLastName()
                .Fill(d => d.EmailAddress, d => $"{d.FirstName}.{d.LastName}@email.com");

            doctors = A.ListOf<Doctor>(NumMockDoctors);
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
