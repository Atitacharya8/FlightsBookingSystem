using FlightsBookingSystem.ReadModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightsBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public class FlightController : ControllerBase
    {


        private readonly ILogger<FlightController> _logger;
        static Random Random = new Random();
        static private FlightRm[] flights = new FlightRm[]
          {
            new (Guid.NewGuid(),
                    "American Airlines",
                    Random.Next(90, 5000). ToString(),
                    new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(Random.Next(1,3))),
                    new TimePlaceRm("Istanbul", DateTime.Now.AddHours(Random.Next(4,10))),
                    Random.Next(1,853)),

           new (Guid.NewGuid(),
                "Deutsche BA",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Schipol", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "American Airlines",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("London, England", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Vizzola-ticino", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "Basiq Air",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("Amsterdam", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Glasgow, Scotland", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "BB Heliag",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("Zurich", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Baku", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "ABA Air",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("Praha Ruzyne", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Paris", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "AB Corporate Aviation",
                Random.Next(90, 5000). ToString(),
                new TimePlaceRm("Le Bourget", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlaceRm("Zagreb", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853))
         };

                public FlightController(ILogger<FlightController> logger)
                {
                    _logger = logger;
                }


        [HttpGet]
        public IEnumerable<FlightRm> Search() => flights;

        [ProducesResponseType(StatusCodes.Status404NotFound)] // or  [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
             

    }
 }