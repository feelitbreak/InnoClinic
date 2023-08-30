namespace InnoClinic.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<T?> GetAsync(int id, CancellationToken cancellationToken);

        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task AddAsync(List<T> entities, CancellationToken cancellationToken);

        void Update(T entity);

        void Remove(T entity);
    }
}
