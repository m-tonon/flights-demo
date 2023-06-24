using Flights.Domain.Entities;

namespace Flights.Data
{
    public class Entities
    {
        public IList<Passenger> Passengers = new List<Passenger>();

        static Random random = new Random();

        public Flight[] Flights = new Flight[]
        {
        new ( Guid.NewGuid(),
              "American Airlines",
              random.Next(90, 5000).ToString(),
              new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1,3))),
              new TimePlace("Istambul", DateTime.Now.AddHours(random.Next(4,10))),
              2),
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

  }
}  