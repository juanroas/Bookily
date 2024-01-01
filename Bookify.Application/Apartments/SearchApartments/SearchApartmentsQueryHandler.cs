using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;
internal sealed class SearchApartmentsQueryHandler : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentsResponse>>
{
    private static readonly int[] ActiveBookingStatuses ={
     (int)BookingStatus.Reserved,
     (int)BookingStatus.Confirmed,
     (int)BookingStatus.Completed

    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<ApartmentsResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentsResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT
                a.id as Id,
                a.name as Name,
                a.description as Description,
                a.price_amount as PriceAmount,
                a.price_currency as PriceCurrency,
                a.address_country as Country,
                a.address_state as State,
                a.address_zip_code as ZipCode,
                a.address_city as City,
                a.address_street as Street
               FROM
                 Apartments a
                WHERE NOT Exist 
                (
                    SELECT 1 
                    FROM Bookings as b
                    WHERE
                        b.apartment_id = a.id AND
                        b.duration_start <= @EndDate AND
                        b.duration_end >= @StartDate
                        b.status = ANY(@ActiveBookingStatuses)
                )";

        var apartments = await connection.
            QueryAsync<ApartmentsResponse, AddressResponse, ApartmentsResponse>(
            sql,
            (apartment, address) =>
            {
                apartment.Address = address;
                return apartment;
            },
            new
            {
                request.StartDate,
                request.EndDate,
                ActiveBookingStatuses
            },
            splitOn: "Country");

        return apartments.ToList();


    }
}

