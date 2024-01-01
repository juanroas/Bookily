namespace Bookify.Application.Bookings.GetBooking;
public class BookingResponse
{
    /// <summary>
    /// For the query response, we want to use primitive types, 
    /// as must as possible to avoid serialization issues and easily to retorn from Database
    /// </summary>
    public Guid Id { get; init; }

    public Guid ApartmentId { get; init; }
    public Guid UserId { get; init; }

    public int Status { get; init; }

    public decimal PriceAmount { get; init; }

    public decimal PriceCurrency { get; init; }

    public decimal CleaningFeeAmount { get; init; }

    public decimal CleaningFeeCurrency { get; init; }

    public decimal AmenitiesUpChargeAmount { get; init; }

    public decimal AmenitiesUpChargeCurrency { get; init; }

    public decimal TotalPriceAmount { get; init; }

    public DateOnly DurationStart { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateOnly CreatedOnUtc { get; init; }
}
