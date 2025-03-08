using UnitTestProject.Interfaces;
using UnitTestProject.Models;

namespace UnitTestProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitTestContext _context;
        public UserRepository(UnitTestContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteUser(int id)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault(u=>u.Id==id));
            return _context.SaveChanges() > 0 ? true : false;
        }

        public User GetUserByID(int id)
        {
            return _context.Users.FirstOrDefault(u=>u.Id==id);
        }

        public ICollection<User> GetUserList()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
