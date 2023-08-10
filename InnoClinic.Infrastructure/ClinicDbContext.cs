using Microsoft.EntityFrameworkCore;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Infrastructure
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
                .IsRequired()
                .HasDefaultValue(string.Empty);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasDefaultValue(string.Empty)
                .HasMaxLength(15);

            modelBuilder.Entity<User>()
                .Property(u => u.ReenteredPassword)
                .HasColumnName("Re-entered Password")
                .IsRequired()
                .HasDefaultValue(string.Empty)
                .HasMaxLength(15);

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}