using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Flights.Data;
using Flights.ReadModels;
using Flights.Dtos;
using Flights.Domain.Errors;

namespace Flights.Controllers;
[ApiController]
[Route("[controller]")]

public class BookingController : ControllerBase
{
  private readonly Entities _entities;

  public BookingController(Entities entities)
  {
    _entities = entities;
  }

  [ProducesResponseType(500)]
  [ProducesResponseType(400)]
  [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
  [HttpGet("{email}")]
  public ActionResult<IEnumerable<BookingRm>> List(string email)
  {
    var bookings = _entities.Flights.ToArray() //take all the flighs change it to an array
      .SelectMany(f => f.Bookings // for each flight take all the bookings
        .Where(b => b.PassengerEmail == email) // filter the bookings by email provided by the parameter
        .Select(b => new BookingRm( // turn every element inse the collection into a BookingRm
          f.Id,
          f.Airline,
          f.Price.ToString(),
          new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
          new TimePlaceRm(f.Departure.Place, f.Departure.Time),
          b.NumberOfSeats,
          email)));

    return Ok(bookings);
  }

  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(500)]
  [HttpDelete]
  public IActionResult Cancel(BookDto dto)
  {
    var flights = _entities.Flights.Find(dto.FlightId);

    var error = flights?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

    if (error == null)
    {
      _entities.SaveChanges();
      return NoContent();
    }

    if (error is NotFoundError)
      return NotFound();

    throw new Exception($"The error of type {error.GetType().Name} occured while canceling the booking made by {dto.PassengerEmail}.");
  }

}
