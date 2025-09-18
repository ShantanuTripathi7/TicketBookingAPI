using Microsoft.EntityFrameworkCore;
using TicketBookingApp.API.Data;
using TicketBookingApp.API.Models;

namespace TicketBookingApp.API.Repositories
{
    public class FlightDetailsRepository : IFlightDetailsRepository
    {
        private readonly AppDbContext _context;

        public FlightDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlightDetails>> GetAllAsync()
        {
            return await _context.FlightDetails.ToListAsync();
        }

        public async Task<FlightDetails?> GetByIdAsync(int id)
        {
            return await _context.FlightDetails.FindAsync(id);
        }

        public async Task AddAsync(FlightDetails ticket)
        {
            _context.FlightDetails.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(FlightDetails ticket)
        {
            var existing = await _context.FlightDetails.FindAsync(ticket.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await _context.FlightDetails.FindAsync(id);
            if (ticket == null) return false;

            _context.FlightDetails.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FlightDetails>> SearchAsync(string? departure, string? destination, DateTime? date, int? minAvailableSeats)
        {
            var query = _context.FlightDetails.AsQueryable();

            if (!string.IsNullOrEmpty(departure))
                query = query.Where(f => f.Departure.ToLower() == departure.ToLower());

            if (!string.IsNullOrEmpty(destination))
                query = query.Where(f => f.Destination.ToLower() == destination.ToLower());

            if (date.HasValue)
                query = query.Where(f => f.DepartureTime.Date == date.Value.Date);

            if (minAvailableSeats.HasValue)
                query = query.Where(f => f.AvailableSeats >= minAvailableSeats.Value);

            return await query.ToListAsync();
        }

    }
}
