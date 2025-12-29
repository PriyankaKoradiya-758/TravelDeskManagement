using System.ComponentModel.DataAnnotations;

namespace TravelDeskManagement.DTOs
{
    public class AddTravelBooking
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Travel Request is required")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Flight Number is required")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "Airline name is required")]
        public string Airline { get; set; }

        [Required(ErrorMessage = "Booking Reference is required")]
        public string BookingReference { get; set; }

        [StringLength(150, ErrorMessage = "Hotel name cannot exceed 150 characters")]
        public string? HotelName { get; set; }
    }
}
