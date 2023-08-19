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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("E-mail")
                .IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}