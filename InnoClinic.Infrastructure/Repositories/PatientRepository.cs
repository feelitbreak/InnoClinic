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
            return await Entity.FirstOrDefaultAsync(p =>
                    ((patientProfile.FirstName.Equals(p.FirstName) && patientProfile.LastName.Equals(p.LastName) && patientProfile.MiddleName.Equals(p.MiddleName))
                    || (patientProfile.FirstName.Equals(p.FirstName) && patientProfile.LastName.Equals(p.LastName) && patientProfile.DateOfBirth.Equals(p.DateOfBirth))
                    || (patientProfile.FirstName.Equals(p.FirstName) && patientProfile.MiddleName.Equals(p.MiddleName) && patientProfile.DateOfBirth.Equals(p.DateOfBirth))
                    || (patientProfile.LastName.Equals(p.LastName) && patientProfile.MiddleName.Equals(p.MiddleName) && patientProfile.DateOfBirth.Equals(p.DateOfBirth)))
                    && !p.IsLinkedToAccount,
                cancellationToken);
        }
    }
}