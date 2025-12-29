using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelDeskManagement.Models
{
    public class TravelBooking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey(nameof(TravelRequest))]
        public int RequestId { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string BookingReference { get; set; }
        public string HotelName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        public TravelRequest TravelRequest { get; set; }
    }
}
