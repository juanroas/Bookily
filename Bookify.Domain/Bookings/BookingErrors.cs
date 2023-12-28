
using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings
{
    public static class BookingErrors
    {
        public static Error NotFound = new("Booking.NotFound", "The Booking with specified identified was not found");
        public static Error OverLap = new("Booking.OverLap", "The current Booking is overlapping with an existing one");
        public static Error NotReserved = new("Booking.NotReserved", "The Booking is not pending");
        public static Error NotConfirmed = new("Booking.NotConfirmed", "The Booking is not confirmed");
        public static Error AlreadyStarted = new("Booking.AlreadyStarted", "The Booking has already started");
    }
}
