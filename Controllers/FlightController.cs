﻿using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
  private readonly ILogger<FlightController> _logger;

  public FlightController(ILogger<FlightController> logger)
  {
    _logger = logger;
  }

  Random random = new Random();

  [HttpGet]
  public IEnumerable<FlightRm> Search()
      => new FlightRm[]
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

}