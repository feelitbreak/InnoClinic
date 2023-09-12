using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Repositories
{
    public class ReceptionistRepository : GenericRepository<Receptionist>, IReceptionistRepository
    {
        public ReceptionistRepository(DbContext context) : base(context)
        {

        }

        public async Task<Receptionist?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await Entity
                .Where(r => r.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
