using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using Microsoft.EntityFrameworkCore;
using InnoClinic.Domain.Extensions;

namespace InnoClinic.Infrastructure.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DbContext context) : base(context)
        {

        }

        public async Task<Patient?> FindMatchingAsync(PatientProfileDto patientProfile,
            CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Matches(patientProfile) && p.IsLinkedToAccount == false, cancellationToken);
        }
    }
}