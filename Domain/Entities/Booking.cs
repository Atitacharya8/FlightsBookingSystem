using System.ComponentModel.DataAnnotations;

namespace FlightsBookingSystem.Domain.Entities
{
    public record Booking(
     
       string PassengerEmail,
       byte NumberOfSeats
        );
}
