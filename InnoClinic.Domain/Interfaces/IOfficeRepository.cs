using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IOfficeRepository : IGenericRepository<Office>
    {
        Task<Office?> GetOfficeWithSpecifiedUserAsync(int officeId, int userId, CancellationToken cancellationToken);
    }
}
