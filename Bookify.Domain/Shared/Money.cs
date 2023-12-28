namespace Bookify.Domain.Shared;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money Second)
    {
        if (first.Currency != Second.Currency)
        {
            throw new InvalidOperationException("Cannot add money of different currencies");
        }

        return new Money(first.Amount + Second.Amount, first.Currency);
    }

    public static Money Zero() => new(0, Currency.None);
    public static Money Zero(Currency currency) => new(0, currency);
    public bool IsZero() => this == Zero(Currency);
}

