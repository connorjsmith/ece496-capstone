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
        public DbSet<Prescription> Prescriptions { get; set; }
        public AppDataContext(DbContextOptions options) : base(options) { }
        public AppDataContext() { } // used for tests only

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships here
            #region Patient
            modelBuilder.Entity<Patient>()
                .HasKey(t => t.PatientId);
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Prescriptions);
            #endregion Patient Relationships

            #region Doctor
            modelBuilder.Entity<Doctor>()
                .HasKey(t => t.DoctorId);
            #endregion

            #region Medication
            modelBuilder.Entity<Medication>()
                .HasKey(t => t.MedicationId);
            #endregion

            #region Prescription
            modelBuilder.Entity<Prescription>()
                .HasKey(p => p.PrescriptionId);
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Medication);
            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.Recurrences);

            modelBuilder.Entity<Prescription.Recurrence>()
                .HasKey(r => r.RecurrenceId);
            modelBuilder.Entity<Prescription.Recurrence>()
                .HasMany(r => r.Doses);

            modelBuilder.Entity<Dose>()
                .HasOne(d => d.AssociatedRecurrence);
            modelBuilder.Entity<Dose>()
                .HasKey(d => d.DoseId);
            #endregion

            #region Patient-Doctor Relationship
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
            #endregion


        }
    }
}
