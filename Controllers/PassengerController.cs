using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Flights.Dtos;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(201)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public IActionResult Register(NewPassengerDto dto)
  {
    throw new NotImplementedException();
  }
}