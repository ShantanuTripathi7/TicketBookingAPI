using Microsoft.EntityFrameworkCore;
using TicketBookingApp.API.Models;

namespace TicketBookingApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FlightDetails> FlightDetails { get; set; } = null!;
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; } = null!;

    }
}
