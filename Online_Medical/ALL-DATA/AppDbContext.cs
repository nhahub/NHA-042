using Microsoft.EntityFrameworkCore;
using Online_Medical.Models;

namespace Online_Medical.ALL_DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }


        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Doctor_WorkingHours> DoctorWorkingHours { get; set; }

        // (DoctorClinic)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<DoctorClinic>()
                .HasKey(dc => new { dc.DoctorId, dc.ClinicId });
        }
    }
}