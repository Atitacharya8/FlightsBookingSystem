using System.ComponentModel.DataAnnotations;

namespace FlightsBookingSystem.Domain.Entities
{
    public record Booking(
       Guid FlightId,
       string PassengerEmail,
       byte NumberOfSeats
        );
}
