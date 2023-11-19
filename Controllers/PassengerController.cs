using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightsBookingSystem.DTOs;
using FlightsBookingSystem.ReadModels;

namespace FlightsBookingSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        static private IList<NewPassengerDTO> Passengers = new List<NewPassengerDTO>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDTO dto)
        {
            Passengers.Add(dto);
            System.Diagnostics.Debug.WriteLine(Passengers.Count);
            return CreatedAtAction(nameof(Find), new {email = dto.Email});
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = Passengers.FirstOrDefault(p => p.Email == email);
            
            if(passenger == null)
            {
                return NotFound();
            }
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.Lastname,
                passenger.Gender
                );

            return Ok(rm);
        }
    }
}
