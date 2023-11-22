using System.ComponentModel.DataAnnotations;

namespace FlightsBookingSystem.Domain.Entities
{

    //DTOS are used to transfer objects or data from one point to other but doesn't store data.
    public record Passenger(
        string Email,
        string FirstName,
        string Lastname,
        bool Gender);
    
}
