namespace Flights.ReadModels
{
  public record BookingRm(
    Guid Id,
    string Airline,
    string Price,
    TimePlaceRm Departure,
    TimePlaceRm Arrival,
    int NumberOfBookedSeats,
    string PassengerEmail);
}