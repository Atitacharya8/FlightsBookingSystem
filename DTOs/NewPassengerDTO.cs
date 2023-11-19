namespace FlightsBookingSystem.DTOs
{

    //DTOS are used to transfer objects or data from one point to other but doesn't store data.
    public record NewPassengerDTO(
        string Email,
        string FirstName,
        string Lastname,
        bool Gender);
    
}
