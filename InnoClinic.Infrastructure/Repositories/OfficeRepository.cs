using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(DbContext context) : base(context)
        {

        }

        public async Task<Office?> GetOfficeAsync(int officeId, int userId, CancellationToken cancellationToken)
        {
            var query = DbSet
                .Where(o => o.Id == officeId)
                .Include(o => o.Users);

            var users = await query.Select(o => o.Users).FirstOrDefaultAsync(cancellationToken);

            return users?.Find(u => u.Id == userId) != null ? await query.FirstOrDefaultAsync(cancellationToken) : null;
        }
    }
}
