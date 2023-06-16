﻿using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly ILogger<FlightController> _logger;

    static Random random = new Random();

    private static FlightRm[] flights = new FlightRm[]
        {
        new ( Guid.NewGuid(),
              "American Airlines",
              random.Next(90, 5000).ToString(),
              new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(random.Next(1,3))),
              new TimePlaceRm("Istambul", DateTime.Now.AddHours(random.Next(4,10))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "Deutsche BA",
              random.Next(90, 5000).ToString(),
              new TimePlaceRm("Munchen", DateTime.Now.AddHours(random.Next(1,10))),
              new TimePlaceRm("Schipol", DateTime.Now.AddHours(random.Next(4,15))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "British Airways",
              random.Next(90, 5000).ToString(),
              new TimePlaceRm("London England", DateTime.Now.AddHours(random.Next(1,15))),
              new TimePlaceRm("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4,18))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "Air France",
              random.Next(90, 5000).ToString(),
              new TimePlaceRm("Paris", DateTime.Now.AddHours(random.Next(1,20))),
              new TimePlaceRm("Munchen", DateTime.Now.AddHours(random.Next(4,25))),
              random.Next(1, 853)),
        new ( Guid.NewGuid(),
              "KLM",
              random.Next(90, 5000).ToString(),
              new TimePlaceRm("Schipol", DateTime.Now.AddHours(random.Next(1,30))),
              new TimePlaceRm("London England", DateTime.Now.AddHours(random.Next(4,35))),
              random.Next(1, 853)),
        };

    private static IList<BookDto> Bookings= new List<BookDto>();

    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
    [HttpGet]
    public IEnumerable<FlightRm> Search()
  => flights;

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

        return Ok(flight);
    }

    [HttpPost]
    public void Book(BookDto dto) 
    {
        System.Diagnostics.Debug.WriteLine($"Booked new flight {dto.FlightId}");
        Bookings.Add(dto);
    }
}
