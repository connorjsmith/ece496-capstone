using PillDispenserWeb.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillDispenserWeb.Models.DataTypes;

namespace PillDispenserWeb.Models.Implementations
{
    public class DoctorRepositoryImpl : IDoctorRepository
    {
        IQueryable<Doctor> _repository;
        public DoctorRepositoryImpl(IQueryable<Doctor> repository)
        {
            _repository = repository;
        }

        public Doctor DoctorById(string doctorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> AllDoctors()
        {
            return _repository.ToList();
        }

        public IEnumerable<Doctor> DoctorsByIds(IEnumerable<string> doctorIds)
        {
            throw new NotImplementedException();
        }
    }
}
