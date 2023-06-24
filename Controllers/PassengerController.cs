using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Flights.Dtos;
using Flights.ReadModels;
using Flights.Domain.Entities;
using Flights.Data;


namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{

  private readonly Entities _entities;

  public PassengerController(Entities entities)
  {
    _entities = entities;
  }

  [ProducesResponseType(201)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  [HttpPost]
  public IActionResult Register(NewPassengerDto dto)
  {
    var newPassenger = new Passenger(
      dto.Email,
      dto.FirstName,
      dto.LastName,
      dto.Gender
    );

    _entities.Passengers.Add(newPassenger);
    return CreatedAtAction(nameof(Find), new { email = dto.Email });
  }

  [HttpGet("{email}")]
  public ActionResult<PassengerRm> Find(string email)
  {
    var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);

    if(passenger == null)
      return NotFound();
    
    var rm = new PassengerRm(
      passenger.Email,
      passenger.FirstName,
      passenger.LastName,
      passenger.Gender
    );

    return Ok(rm);
  }
}