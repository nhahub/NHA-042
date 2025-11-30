using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Medical.Models;

namespace Online_Medical.ALL_DATA
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {


        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }


        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }

        // (DoctorClinic)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<DoctorClinic>()
              .HasKey(dc => new { dc.DoctorId, dc.ClinicId });
            //   modelBuilder.Entity<Doctor>()
            //.HasOne(d => d.ApplicationUser)
            //.WithOne(u => u.DoctorData)
            //.HasForeignKey<Doctor>(d => d.Id); // <--- يستخدم المفتاح "Id" كـFK

            //   // ===========================================
            //   // 🛑 2. تحديد المفتاح المركب لـDoctorClinic
            //   // ===========================================
            //   modelBuilder.Entity<DoctorClinic>()
            //       .HasKey(dc => new { dc.DoctorId, dc.ClinicId });

            //   // 🛑 3. العلاقة بين DoctorClinic و Doctor
            //   modelBuilder.Entity<DoctorClinic>()
            //       .HasOne(dc => dc.Doctor)
            //       .WithMany(d => d.DoctorClinics)
            //       .HasForeignKey(dc => dc.DoctorId) // <--- هنا نستخدم "DoctorId"
            //       .OnDelete(DeleteBehavior.Cascade);

            //   // 🛑 4. العلاقة بين Appointment و Doctor (للتأكد من عدم وجود تعارض)
            //   modelBuilder.Entity<Appointment>()
            //       .HasOne(a => a.Doctor)
            //       .WithMany(d => d.Appointments)
            //       .HasForeignKey(a => a.DoctorId) // <--- هنا يجب أن يكون المفتاح الخارجي في Appointment
            //       .OnDelete(DeleteBehavior.Restrict); // نستخدم Restrict أو NoAction لحماية Identity

            //delete patient cascade appointments
            modelBuilder.Entity<Appointment>()
        .HasOne(a => a.Patient)
        .WithMany(p => p.Appointments)
        .HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.Cascade);


            // 2) Cascade delete for Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}