using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        private readonly ClinicDbContext _context;

        public OfficeRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Office?> Get(int userId, int officeId, bool isActive, CancellationToken cancellationToken)
        {
            var query = _context.Users.Where(u => u.Id == userId && u.OfficeId == officeId);

            if (isActive)
            {
                query = query.Where(t => t.Office != null && t.Office.IsActive);
            }

            return await query.Include(o => o.Office)
                .Select(t=>t.Office)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
