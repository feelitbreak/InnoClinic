using InnoClinic.Domain.Entities;
using System.Collections.Generic;

namespace InnoClinic.Domain.Interfaces
{
    public interface IReceptionistRepository :  IGenericRepository<Receptionist>
    {
        Task<Receptionist?> GetReceptionistAsync(int userId, CancellationToken cancellationToken);
    }
}
