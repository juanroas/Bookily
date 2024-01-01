
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Dapper;

namespace Bookify.Application.Bookings.GetBooking;
internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT
        b.id as Id,
        b.ApartmentId as ApartmentId, 
        b.user_id as UserId, 
        b.status as Status, 
        b.price_for_period_amount as PriceAmount, 
        b.price_for_period_currency as PriceCurrency, 
        b.cleaning_fee_amount as CleaningFeeAmount, 
        b.cleaning_fee_currency as  CleaningFeeCurrency,
        b.amenities_up_charge_amount as AmenitiesUpChargeAmount,
        b.amenities_up_charge_currency as AmenitiesUpChargeCurrency,
        b.total_price_amount as TotalPriceAmount,
        b.duration_start as DurationStart,
        b.duration_end as DurationEnd,
        b.created_on_utc as CreatedOnUtc
            FROM
                Bookings b
            WHERE
                b.id = @BookingId";

        var booking = await connection.QuerySingleOrDefaultAsync<BookingResponse>(
                       sql,
                       new
                       {
                           request.BookingId
                       });
        return booking;
    }
}

