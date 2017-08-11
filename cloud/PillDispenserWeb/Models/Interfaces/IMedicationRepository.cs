using PillDispenserWeb.Models.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Interfaces
{
    interface IMedicationRepository
    {
        Medication MedicationById(string medicationId);
        IEnumerable<Medication> MedicationsByIds(IEnumerable<string> medicationIds);
        IEnumerable<Medication> MedicationsByPartialName(string name);
    }
}
