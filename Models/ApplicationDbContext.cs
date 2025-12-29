using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TravelDeskManagement.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }
        public DbSet<TravelBooking> TravelBookings { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

    }
}
