using System.ComponentModel.DataAnnotations;
using TravelDeskManagement.Helpers;

namespace TravelDeskManagement.DTOs
{
    public class AddTravelRequest : IValidatableObject
    {
        public int RequestId { get; set; }
        [Required(ErrorMessage = "Employee is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        public TravelPurpose Purpose { get; set; }

        public string? OtherPurpose { get; set; }

        [Required(ErrorMessage = "Travel From location is required")]
        [StringLength(100)]
        public string TravelFrom { get; set; }

        [Required(ErrorMessage = "Travel To location is required")]
        [StringLength(100)]
        public string TravelTo { get; set; }

        [Required(ErrorMessage = "Departure date is required")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Return date is required")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Required]
        public TravelRequestStatus Status { get; set; } = TravelRequestStatus.Pending;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReturnDate < DepartureDate)
            {
                yield return new ValidationResult(
                    "Return date cannot be earlier than Departure date",
                    new[] { nameof(ReturnDate) }
                );
            }
        }
    }
}
