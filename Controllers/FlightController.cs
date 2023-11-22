using FlightsBookingSystem.ReadModels;
using FlightsBookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlightsBookingSystem.DTOs;
using FlightsBookingSystem.Domain.Errors;

namespace FlightsBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {


        private readonly ILogger<FlightController> _logger;
        static Random Random = new Random();
        static private Flight[] flights = new Flight[]
          {
            new (Guid.NewGuid(),
                    "American Airlines",
                    Random.Next(90, 5000). ToString(),
                    new TimePlace("Los Angeles", DateTime.Now.AddHours(Random.Next(1,3))),
                    new TimePlace("Istanbul", DateTime.Now.AddHours(Random.Next(4,10))),
                    2),

           new (Guid.NewGuid(),
                "Deutsche BA",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Munchen", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Schipol", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "American Airlines",
                Random.Next(90, 5000). ToString(),
                new TimePlace("London, England", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Vizzola-ticino", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "Basiq Air",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Amsterdam", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "BB Heliag",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Zurich", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Baku", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "ABA Air",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Paris", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853)),

           new (Guid.NewGuid(),
                "AB Corporate Aviation",
                Random.Next(90, 5000). ToString(),
                new TimePlace("Le Bourget", DateTime.Now.AddHours(Random.Next(1,3))),
                new TimePlace("Zagreb", DateTime.Now.AddHours(Random.Next(4,10))),
                Random.Next(1,853))
         };



                public FlightController(ILogger<FlightController> logger)
                {
                    _logger = logger;
                }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
         [ProducesResponseType(typeof(FlightRm), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = flights.Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                 flight.RemainingNumberOfSeats
                )).ToArray();

            return flightRmList;


        }

        [ProducesResponseType(StatusCodes.Status404NotFound)] // or  [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        [HttpGet("{id}")]
        //using ActionResult for GET method so need to use <Flight> type
        public ActionResult<FlightRm> Find(Guid id) 
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return NotFound();
            }

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                 flight.RemainingNumberOfSeats
                );

            return Ok(readModel);
        }

        //using IActionResult for POST method so no need to use any type
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDTO dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");
            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);
            
            if(flight == null)
            {
                return NotFound();
            }

           var error =  flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
                return Conflict(new { message = "Not Enough seats" });
            
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }

    }
 }