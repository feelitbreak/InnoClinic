using Microsoft.EntityFrameworkCore;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Infrastructure.Implementation
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Receptionist> Receptionists { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}