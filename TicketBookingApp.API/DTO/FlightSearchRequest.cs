public class FlightSearchRequest
{
    public string? Departure { get; set; }
    public string? Destination { get; set; }
    public DateTime? Date { get; set; }
    public int? MinAvailableSeats { get; set; }
}
