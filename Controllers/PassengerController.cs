using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Flights.Dtos;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
  static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();

  [ProducesResponseType(201)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  [HttpPost]
  public IActionResult Register(NewPassengerDto dto)
  {
    Passengers.Add(dto);
    System.Diagnostics.Debug.WriteLine(Passengers.Count);
    return Ok();
  }
}