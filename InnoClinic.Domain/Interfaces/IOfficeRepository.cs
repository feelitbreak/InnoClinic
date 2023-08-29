using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IOfficeRepository : IGenericRepository<Office>
    {
        Task<Office?> GetAsync(int officeId, int userId, CancellationToken cancellationToken);
    }
}
