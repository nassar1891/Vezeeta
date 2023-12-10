using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VezeetaAPI.Models
{
    public partial class VezeetaContext : DbContext
    {
        public VezeetaContext()
        {
        }

        public VezeetaContext(DbContextOptions<VezeetaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=VEZEETA;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ApplicationUsers");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DateOfBirth).HasColumnType("date");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);

                // Define the relationship with Doctor (ApplicationUser has Doctor reference)
                entity.HasOne(d => d.Doctor)
                    .WithOne(a => a.ApplicationUser)
                    .HasForeignKey<Doctor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_ApplicationUsers");

                // Define the relationship with Patient (ApplicationUser has Patient reference)
                entity.HasOne(p => p.Patient)
                    .WithOne(a => a.ApplicationUser)
                    .HasForeignKey<Patient>(p => p.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_ApplicationUsers");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Bookings");

                // ... other configurations
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Doctors");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Specialization).HasMaxLength(100);

                // Define the relationship with ApplicationUser (Doctor has ApplicationUser reference)
                entity.HasOne(d => d.ApplicationUser)
                    .WithOne(a => a.Doctor)
                    .HasForeignKey<Doctor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_ApplicationUsers");

                // ... other configurations
            });

            // Define other entity configurations

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
