using System.ComponentModel.DataAnnotations;

namespace TicketBookingApp.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FlightDetailsId { get; set; }

        [Required]
        public int SeatsBooked { get; set; }

        public DateTime BookingTime { get; set; } = DateTime.UtcNow;

        // Navigation properties (optional, for EF Core)
        public User? User { get; set; }
        public FlightDetails? FlightDetails { get; set; }
    }
}
