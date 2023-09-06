using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IOfficeRepository : IGenericRepository<Office>
    {
        Task<Office?> GetOfficeAsync(int officeId, int userId, CancellationToken cancellationToken);
    }
}
