using InnoClinic.Domain.Interfaces;
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

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<Office?> GetOfficeAsync(int userId, int officeId, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(u => u.Id == userId && u.OfficeId == officeId)
                .Include(u => u.Office)
                .Select(u => u.Office)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
