using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IReceptionistRepository :  IGenericRepository<Receptionist>
    {
        Task<Receptionist?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    }
}
