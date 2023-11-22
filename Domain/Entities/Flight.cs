using FlightsBookingSystem.Domain.Errors;
using FlightsBookingSystem.ReadModels;

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
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;


        // "object" keyword used to refer the OverbookError.cs file here
        internal object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;

            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }

            flight.Bookings.Add(
                new Booking(
                   passengerEmail,
                    numberOfSeats
            ));

            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }

       }
   
}
