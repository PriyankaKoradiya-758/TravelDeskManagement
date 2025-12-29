using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelDeskManagement.DTOs;
using TravelDeskManagement.Models;

namespace TravelDeskManagement.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminUsers.Where(x => !x.IsDeleted).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .FirstOrDefaultAsync(m => m.AdminUserId == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View(adminUser);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminUserId,Username")] AddAdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                string Hashed = BCrypt.Net.BCrypt.HashPassword("Admin@123");
                AdminUser obj = new AdminUser
                {
                    PasswordHash = Hashed,
                    Username = adminUser.Username,
                    CreatedBy = adminUserId.Value,
                    CreatedDate = DateTime.Now
                };
                _context.AdminUsers.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminUser);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser == null)
            {
                return NotFound();
            }

            AddAdminUser obj = new AddAdminUser
            {
                Username = adminUser.Username,
                AdminUserId = adminUser.AdminUserId
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminUserId,Username")] AddAdminUser adminUser)
        {
            if (id != adminUser.AdminUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _context.AdminUsers.FindAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                    AdminUser obj = new AdminUser
                    {
                        AdminUserId = adminUser.AdminUserId,
                        Username = adminUser.Username,
                        PasswordHash = employee.PasswordHash,
                        UpdatedBy = adminUserId,
                        UpdatedDate = DateTime.Now
                    };
                    _context.AdminUsers.Update(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminUserExists(adminUser.AdminUserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminUser);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .FirstOrDefaultAsync(m => m.AdminUserId == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View(adminUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser != null)
            {
                adminUser.IsDeleted = true;
                _context.AdminUsers.Update(adminUser);
            }
              
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminUserExists(int id)
        {
            return _context.AdminUsers.Any(e => e.AdminUserId == id);
        }
    }
}
