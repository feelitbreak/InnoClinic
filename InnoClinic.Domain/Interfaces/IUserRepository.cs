using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<Office?> GetOfficeAsync(int userId, int officeId, CancellationToken cancellationToken);
    }
}
