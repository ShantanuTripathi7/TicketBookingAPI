using TicketBookingApp.API.Models;

namespace TicketBookingApp.API.Repositories
{
    public interface IFlightDetailsRepository
    {
        Task<IEnumerable<FlightDetails>> GetAllAsync();
        Task<FlightDetails?> GetByIdAsync(int id);
        Task AddAsync(FlightDetails ticket);
        Task<bool> UpdateAsync(FlightDetails ticket);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FlightDetails>> SearchAsync(string? departure, string? destination, DateTime? date, int? minAvailableSeats);


    }
}
