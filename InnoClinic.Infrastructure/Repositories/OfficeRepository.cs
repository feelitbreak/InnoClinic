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
            var query = Entity
                .Where(o => o.Id == officeId)
                .Include(o => o.Receptionists);

            var receptionists = await query.Select(o => o.Receptionists).FirstOrDefaultAsync(cancellationToken);

            return receptionists?.Find(r => r.UserId == userId) != null ? await query.FirstOrDefaultAsync(cancellationToken) : null;
        }
    }
}
