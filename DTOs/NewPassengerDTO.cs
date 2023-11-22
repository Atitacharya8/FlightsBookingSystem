using System.ComponentModel.DataAnnotations;

namespace FlightsBookingSystem.DTOs
{

    //DTOS are used to transfer objects or data from one point to other but doesn't store data.
    public record NewPassengerDTO(
        [Required][EmailAddress][StringLength(100, MinimumLength = 3)] string Email,
        [Required][MinLength(2)][MaxLength(35)] string FirstName,
         [Required][MinLength(2)][MaxLength(35)] string Lastname,
         [Required] bool Gender);

}
