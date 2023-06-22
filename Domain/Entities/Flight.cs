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

    public object? MakeBooking(string passengerEmail, byte numberOfSeats)
    {
      var flight = this;

      if(flight.RemainingSeats < numberOfSeats)
            // return Conflict(new { message = "The number of requested seats exceeds the number of remaining seats." });
            return new OverbookError();
        
        var booking = new Booking(
            passengerEmail,
            numberOfSeats);

        flight.Bookings.Add(booking);

        flight.RemainingSeats -= numberOfSeats;
        return null;
    }
  }
}