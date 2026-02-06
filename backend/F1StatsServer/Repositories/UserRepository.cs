using F1StatsServer.Data;
using F1StatsServer.Dto.UserDto;
using F1StatsServer.Infrastructure;
using F1StatsServer.Interfaces;
using F1StatsServer.Entities;

namespace F1StatsServer.Repositories
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        private readonly AdventureContext _context;

        public UserRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckCredentials(string email, string password)
        {
            return _context.Users.Any(o => o.Email == email && o.Password == password);
        }

        public bool RegisterUser(RegisterDto data)
        {
            bool result = CreateItem(data);

            if (!result)
                return false;
            return true;
        }

        public bool CreateItem(RegisterDto data)
        {
            User user = new()
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                IsAdmin = false
            };
            if (_context.Users.Contains(user))
                return false;
            _context.AddAsync(user);

            return _context.SaveChanges() > 0;
        }

        public User CheckRole(string email, string password)
        {
            return _context.Users.Where((c) => c.Email == email && c.Password == password).FirstOrDefault();
        }
    }
}
