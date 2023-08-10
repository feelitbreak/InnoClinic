using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();

        public User? GetUserById(int id);

        public void AddUser(User user);

        public bool CheckUser(int id);
    }
}
