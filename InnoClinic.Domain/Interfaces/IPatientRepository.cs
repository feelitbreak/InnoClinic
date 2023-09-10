using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> FindMatchingAsync(PatientProfileDto patientProfile,
            CancellationToken cancellationToken);
    }
}