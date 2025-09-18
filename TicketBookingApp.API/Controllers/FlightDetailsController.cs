using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.API.Models;
using TicketBookingApp.API.Repositories;

namespace TicketBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailsController : ControllerBase
    {
        private readonly IFlightDetailsRepository _repo;

        public FlightDetailsController(IFlightDetailsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/FlightDetails
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _repo.GetAllAsync();
            return Ok(tickets);
        }

        // GET: api/FlightDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _repo.GetByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        // POST: api/FlightDetails
        [HttpPost]
        [Authorize] // requires JWT
        public async Task<IActionResult> CreateTicket([FromBody] FlightDetails ticket)
        {
            await _repo.AddAsync(ticket);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        // PUT: api/FlightDetails/5
        [HttpPut("{id}")]
        [Authorize] // requires JWT
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] FlightDetails ticket)
        {
            if (id != ticket.Id) return BadRequest();

            var updated = await _repo.UpdateAsync(ticket);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/FlightDetails/5
        [HttpDelete("{id}")]
        [Authorize] // requires JWT
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        // GET: api/FlightDetails/search
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] FlightSearchRequest request)
        {
            var results = await _repo.SearchAsync(request.Departure, request.Destination, request.Date, request.MinAvailableSeats);
            return Ok(results);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableFlights([FromQuery] string? departure, [FromQuery] string? destination)
        {
            var flights = await _repo.GetAllAsync();

            var filtered = flights.Where(f => f.AvailableSeats > 0);

            if (!string.IsNullOrEmpty(departure))
                filtered = filtered.Where(f => f.Departure.Equals(departure, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(destination))
                filtered = filtered.Where(f => f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase));

            return Ok(filtered);
        }




    }
}
