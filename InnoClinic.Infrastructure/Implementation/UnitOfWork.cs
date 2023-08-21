﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicDbContext _context;
        private IUserRepository? _userRepository;
        private bool _disposed;

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public UnitOfWork(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
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
