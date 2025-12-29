using System.ComponentModel.DataAnnotations;
using TravelDeskManagement.Helpers;

namespace TravelDeskManagement.DTOs
{
    public class AddEmployee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Code is required")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(200, ErrorMessage = "Full Name cannot exceed 200 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public Department Department { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
