using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Infrastructure.Repositories
{
    public class ReceptionistRepository : GenericRepository<Receptionist>, IReceptionistRepository
    {
        public ReceptionistRepository(DbContext context) : base(context)
        {

        }

        public async Task<Receptionist?> GetReceptionistAsync(int userId, CancellationToken cancellationToken)
        {
            return await DbSet
                .Where(r => r.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
