namespace Flights.Domain.Entities
{
  public record Flight(
    Guid Id,
    string Airline,
    string Price,
    TimePlace Departure,
    TimePlace Arrival,
    int RemainingSeats
    )
  {
    public IList<Booking> Bookings = new List<Booking>();
    public int RemainingSeats { get; set;} = RemainingSeats;
  }
}