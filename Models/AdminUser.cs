using System.ComponentModel.DataAnnotations;

namespace TravelDeskManagement.Models
{
    public class AdminUser
    {
        [Key]
        public int AdminUserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
