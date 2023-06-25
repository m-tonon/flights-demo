using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Flights.Data;
using Flights.ReadModels;

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
}
