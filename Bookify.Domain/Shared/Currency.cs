﻿using System.Collections.Generic;

namespace Bookify.Domain.Shared;
public record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public string Code { get; init; }
    private Currency(string code) => Code = code;

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException($"Currency {code} is not supported");
    }

    public static readonly IReadOnlyCollection<Currency> All = new[] { Usd, Eur };

}

