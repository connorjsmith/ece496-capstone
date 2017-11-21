using PillDispenserWeb.Models.DataTypes;
using Microsoft.EntityFrameworkCore;
using PillDispenserWeb.Models.Relations;

namespace PillDispenserWeb.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientDoctor> PatientDoctor { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public AppDataContext(DbContextOptions options) : base(options) { }
        public AppDataContext() { } // used for tests only

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships here
            modelBuilder.Entity<Patient>()
                .HasKey(t => t.PatientId);

            modelBuilder.Entity<Doctor>()
                .HasKey(t => t.DoctorId);

            modelBuilder.Entity<Medication>()
                .HasKey(t => t.MedicationId);

            modelBuilder.Entity<PatientDoctor>()
                .HasKey(t => new { t.PatientId, t.DoctorId});

            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.Doctors)
                .HasForeignKey(pd => pd.PatientId);

            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Doctor)
                .WithMany(p => p.Patients)
                .HasForeignKey(pd => pd.DoctorId);
        }
    }
}
