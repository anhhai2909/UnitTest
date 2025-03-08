using UnitTestProject.Models;

namespace UnitTestProject.Interfaces
{
    public interface IUserRepository
    {
        public ICollection<User> GetUserList();
        public User GetUserByID(int id);
        public bool UpdateUser(User user);
        public bool CreateUser(User user);
        public bool DeleteUser(int id);

    }
}
