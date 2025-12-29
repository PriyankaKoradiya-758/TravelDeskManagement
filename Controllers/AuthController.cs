using Microsoft.AspNetCore.Mvc;
using TravelDeskManagement.Helpers;
using TravelDeskManagement.Repository.Auth;
using TravelDeskManagement.Services;

namespace TravelDeskManagement.Controllers
{
    [JwtAuthorize]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IJWTServices _tokenService;

        public AuthController(IAuthRepository repo, IJWTServices tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        public IActionResult Login()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _repo.ValidateUser(username, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
                return View();
            }

            var token = _tokenService.GenerateToken(user);
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetInt32("AdminUserId", user.AdminUserId);

            return RedirectToAction("Index", "Employees");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
