using Microsoft.EntityFrameworkCore;
using TicketBookingApp.API.Data;
using TicketBookingApp.API.Models;

namespace TicketBookingApp.API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Set<Booking>()
                                 .Include(b => b.User)
                                 .Include(b => b.FlightDetails)
                                 .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Set<Booking>()
                                 .Include(b => b.User)
                                 .Include(b => b.FlightDetails)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking?> CreateBookingAsync(Booking booking)
        {
            // Check if flight exists and has enough available seats
            var flight = await _context.FlightDetails.FindAsync(booking.FlightDetailsId);
            if (flight == null || flight.AvailableSeats < booking.SeatsBooked)
                return null;

            // Reduce available seats
            flight.AvailableSeats -= booking.SeatsBooked;

            _context.Set<Booking>().Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Set<Booking>().FindAsync(id);
            if (booking == null) return false;

            // Restore seats
            var flight = await _context.FlightDetails.FindAsync(booking.FlightDetailsId);
            if (flight != null)
            {
                flight.AvailableSeats += booking.SeatsBooked;
            }

            _context.Set<Booking>().Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
