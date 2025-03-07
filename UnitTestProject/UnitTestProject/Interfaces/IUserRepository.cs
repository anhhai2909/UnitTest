using UnitTestProject.Models;

namespace UnitTestProject.Interfaces
{
    public interface IUserRepository
    {
        public ICollection<User> GetUserList();
        public User GetUserByID(short id);
        public bool UpdateUser(User user);
        public User CreateUser(User user);
        public bool DeleteUser(short id);

    }
}
