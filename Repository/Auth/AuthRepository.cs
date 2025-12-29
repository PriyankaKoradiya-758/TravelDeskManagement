using BCrypt.Net;
using TravelDeskManagement.Models;

namespace TravelDeskManagement.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AdminUser ValidateUser(string username, string password)
        {
            var user = _context.AdminUsers
        .FirstOrDefault(x => x.Username == username);

            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return isValid ? user : null;
        }
    }   
}
