using TicketBookingApp.API.Models;

namespace TicketBookingApp.API.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking?> CreateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
