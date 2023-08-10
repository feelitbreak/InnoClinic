using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Repositories;
using InnoClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClinicDbContext _context;

        public UserRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool CheckUser(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
