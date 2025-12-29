using TravelDeskManagement.Models;

namespace TravelDeskManagement.Repository.Auth
{
    public interface IAuthRepository
    {
        AdminUser ValidateUser(string username, string password);

    }
}
