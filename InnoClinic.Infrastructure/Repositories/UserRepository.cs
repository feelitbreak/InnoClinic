using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using InnoClinic.Infrastructure.Implementation;

namespace InnoClinic.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ClinicDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.SingleOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
