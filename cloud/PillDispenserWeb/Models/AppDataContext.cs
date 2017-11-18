using PillDispenserWeb.Models.DataTypes;
using Microsoft.EntityFrameworkCore;

namespace PillDispenserWeb.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public AppDataContext(DbContextOptions options) : base(options) { }
        public AppDataContext() { } // used for tests only

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships here
            modelBuilder.Entity<Doctor>()
                .HasMany<Patient>(d => d.Patients);

            modelBuilder.Entity<Patient>()
                .HasMany<Doctor>(p => p.Doctors);
        }
    }
}
