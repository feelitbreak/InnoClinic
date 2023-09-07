using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using InnoClinic.Infrastructure.Implementation;

namespace InnoClinic.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }

        public async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
        {
            return await DbSet.AnyAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Email.Equals(email) && u.IsActive, cancellationToken);
        }

        public async Task<User?> FindPatientProfileAsync(PatientProfileDto patientProfile,
            CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Matches(patientProfile) && u.IsLinkedToAccount == false, cancellationToken);
        }
    }
}
