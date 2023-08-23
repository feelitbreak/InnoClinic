﻿using InnoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAsync();

        Task<T?> GetAsync(int id);

        Task AddAsync(T entity);

        Task AddAsync(List<T> entities);

        void Update(T entity);

        void Remove(T entity);
    }
}
