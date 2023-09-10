namespace InnoClinic.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IOfficeRepository Offices { get; }

        IPatientRepository Patients { get; }

        IReceptionistRepository Receptionists { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
