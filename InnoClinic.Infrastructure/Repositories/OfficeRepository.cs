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
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}
