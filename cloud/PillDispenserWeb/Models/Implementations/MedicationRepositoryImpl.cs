using PillDispenserWeb.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PillDispenserWeb.Models.DataTypes;

namespace PillDispenserWeb.Models.Implementations
{
    public class MedicationRepositoryImpl : IMedicationRepository
    {
        public Medication MedicationById(string medicationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medication> MedicationsByIds(IEnumerable<string> medicationIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medication> MedicationsByPartialName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
