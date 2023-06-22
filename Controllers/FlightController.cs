using Flights.ReadModels;
using Flights.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly ILogger<FlightController> _logger;

    static Random random = new Random();

    private static Flight[] flights = new Flight[]
        {
        new ( Guid.NewGuid(),
              "American Airlines",
              random.Next(90, 5000).ToString(),
              new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1,3))),
              new TimePlace("Istambul", DateTime.Now.AddHours(random.Next(4,10))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "Deutsche BA",
              random.Next(90, 5000).ToString(),
              new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1,10))),
              new TimePlace("Schipol", DateTime.Now.AddHours(random.Next(4,15))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "British Airways",
              random.Next(90, 5000).ToString(),
              new TimePlace("London England", DateTime.Now.AddHours(random.Next(1,15))),
              new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4,18))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "Air France",
              random.Next(90, 5000).ToString(),
              new TimePlace("Paris", DateTime.Now.AddHours(random.Next(1,20))),
              new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(4,25))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "KLM",
              random.Next(90, 5000).ToString(),
              new TimePlace("Schipol", DateTime.Now.AddHours(random.Next(1,30))),
              new TimePlace("London England", DateTime.Now.AddHours(random.Next(4,35))),
              random.Next(1, 853)),
        };

    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
    [HttpGet]
    public IEnumerable<FlightRm> Search()
    {
        var flightRmList = flights.Select(flight => new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
            new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
            flight.RemainingSeats
        ));

        return flightRmList;
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(FlightRm), 200)]
    [HttpGet("{id}")]
    public ActionResult<FlightRm> Find(Guid id)
    {
        var flight = flights.SingleOrDefault(f => f.Id == id);

        if (flight == null)
            return NotFound();

        var readModel = new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
            new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
            flight.RemainingSeats
        );

        return Ok(readModel);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(200)]
    [HttpPost]
    public IActionResult Book(BookDto dto)
    {
        System.Diagnostics.Debug.WriteLine($"Booked new flight {dto.FlightId}");
        
        var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

        if (flight == null)
            return NotFound();
        
        var booking = new Booking(
            dto.FlightId,
            dto.PassengerEmail,
            dto.NumberOfSeats);

        flight.Bookings.Add(booking);
        return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
    }

}
