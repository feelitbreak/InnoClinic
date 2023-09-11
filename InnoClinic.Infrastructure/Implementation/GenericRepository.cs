using InnoClinic.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> Entity;

        public GenericRepository(DbContext context)
        {
            Entity = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Entity.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await Entity.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Entity.AddAsync(entity, cancellationToken);
        }

        public async Task AddAsync(List<T> entities, CancellationToken cancellationToken)
        {
            await Entity.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            Entity.Update(entity);
        }

        public void Remove(T entity)
        {
            Entity.Remove(entity);
        }
    }
}
