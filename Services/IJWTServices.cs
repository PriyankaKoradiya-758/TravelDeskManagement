using TravelDeskManagement.Models;

namespace TravelDeskManagement.Services
{
    public interface IJWTServices
    {
        string GenerateToken(AdminUser user);

    }
}
