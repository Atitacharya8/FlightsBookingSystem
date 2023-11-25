using FlightsBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightsBookingSystem.Data
{
    public class Entities: DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();

        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions<Entities> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            //To avoid Race Condition for overbooking
            modelBuilder.Entity<Flight>().Property(p=>p.RemainingNumberOfSeats)
                .IsConcurrencyToken();

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);

            //one flight has many bookings
            modelBuilder.Entity<Flight>().OwnsMany(f => f.Bookings);
        }

    }
}
