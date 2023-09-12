﻿using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
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
            return await Entity.AnyAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await Entity.SingleOrDefaultAsync(u => u.Email.Equals(email) && u.IsActive, cancellationToken);
        }

        public async Task<User?> GetPatientUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await Entity
                .Include(u => u.Patient)
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }
    }
}
