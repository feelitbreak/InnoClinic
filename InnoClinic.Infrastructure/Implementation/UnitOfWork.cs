using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Repositories;

namespace InnoClinic.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicDbContext _context;
        private IUserRepository? _userRepository;
        private IOfficeRepository? _officeRepository;
        private bool _disposed;

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IOfficeRepository Offices => _officeRepository ??= new OfficeRepository(_context);

        public UnitOfWork(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
