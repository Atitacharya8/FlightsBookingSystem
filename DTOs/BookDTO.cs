namespace FlightsBookingSystem.DTOs
{
    public record BookDTO(
        Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats
        );
}
