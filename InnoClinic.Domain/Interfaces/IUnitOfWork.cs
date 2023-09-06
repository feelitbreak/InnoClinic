namespace InnoClinic.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IOfficeRepository Offices { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
