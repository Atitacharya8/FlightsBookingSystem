namespace FlightsBookingSystem.Domain.Entities
{
    public record Flight(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        )

       {
            //to store the bookings 
        public IList<Booking> Bookings = new List<Booking>();
       }
   
}
