using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Interfaces
{
    public interface IReceptionistRepository :  IGenericRepository<Receptionist>
    {
        Task<Receptionist?> GetReceptionistAsync(int userId, CancellationToken cancellationToken);
    }
}
