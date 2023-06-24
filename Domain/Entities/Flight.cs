using Flights.Domain.Errors;
namespace Flights.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        public Flight()
        {
        }

        public Flight(
                Guid id,
                string airline,
                string price,
                TimePlace departure,
                TimePlace arrival,
                int remainingSeats
            )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingSeats = remainingSeats;
        }

        public object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;

            if (flight.RemainingSeats < numberOfSeats)
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