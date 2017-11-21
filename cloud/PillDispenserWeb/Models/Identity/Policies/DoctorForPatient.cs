using Microsoft.AspNetCore.Authorization;
using PillDispenserWeb.Models.DataTypes;
using PillDispenserWeb.Models.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PillDispenserWeb.Models.Identity.Policies
{
    public class DoctorForPatientRequirement : IAuthorizationRequirement { }
    public class DoctorForPatientHandler : AuthorizationHandler<DoctorForPatientRequirement, Patient>
    {
        private readonly AppDataContext appDataContext;
        DoctorForPatientHandler(AppDataContext _appDataContext)
        {
            appDataContext = _appDataContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DoctorForPatientRequirement requirement, Patient patient)
        {
            Console.WriteLine("Hello World");
            // user must be a doctor
            if (/*!context.User.IsInRole("Doctor") || */context.User.Identity is null)
            {
                return Task.CompletedTask; // don't fail automatically, might pass another requirement
            }

            var doctor = appDataContext.Doctors.First(d => d.EmailAddress == context.User.Identity.Name);

            bool isDoctorForPatient = !(appDataContext.PatientDoctor.Find(new PatientDoctor { PatientId = patient.PatientId, DoctorId = doctor.DoctorId }) is null);

            if (isDoctorForPatient)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
