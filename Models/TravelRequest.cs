using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelDeskManagement.Helpers;

namespace TravelDeskManagement.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public TravelPurpose Purpose { get; set; }
        public string? OtherPurpose { get; set; }
        public string TravelFrom { get; set; }
        public string TravelTo { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public TravelRequestStatus Status { get; set; } = TravelRequestStatus.Pending;
        public Employee Employee { get; set; }
    }
}
